using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Constants;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.Services
{
    public class BlogService : IBlogService
    {
        private WAGDbContext DbContext;
        private IFileService CommonService;

        public BlogService(WAGDbContext dbContext, IFileService commonService)
        {
            this.DbContext = dbContext;
            this.CommonService = commonService;
        }
        
        public void CreateArticle(CreateArticleViewModel createArticleViewModel)
        {
            var articleNew = new Article();

            articleNew.Title = createArticleViewModel.Title;
            articleNew.ShortDescription = createArticleViewModel.ShortDescription;
            articleNew.ArticleContentFileName = this.UploadArticleContent(createArticleViewModel.ArticleContent);

            if (createArticleViewModel.MainPicture != null)
            {
                var imgFileName = $"{Guid.NewGuid()}{GlobalConstants.jpegFileExtension}";

                articleNew.MainPictureFileName = this.CommonService.UploadImageAsync(GlobalConstants.articlesImageDirectoryPath, imgFileName, createArticleViewModel.MainPicture).Result;
            }
            
            articleNew.CreatedOn = DateTime.UtcNow;

            this.DbContext.Articles.Add(articleNew);
            this.DbContext.SaveChanges();
        }

        public void EditArticle(int id, EditArticleViewModel editArticleViewModel)
        {
            if (this.DbContext.Articles.Any(a => a.Id == id))
            {
                this.DbContext.Articles.First(a => a.Id == id).Title = editArticleViewModel.Title;
                this.DbContext.Articles.First(a => a.Id == id).ShortDescription = editArticleViewModel.ShortDescription;
                var articleContentFileName = this.DbContext.Articles.First(a => a.Id == id).ArticleContentFileName;
                this.UploadArticleContent(editArticleViewModel.ArticleContent, articleContentFileName);

                if (editArticleViewModel.MainPicture != null)
                {
                    var oldImgFileName = this.DbContext.Articles.First(a => a.Id == id).MainPictureFileName;

                    if (File.Exists($"{GlobalConstants.articlesImageDirectoryPath}{oldImgFileName}"))
                    {
                        File.Delete($"{GlobalConstants.articlesImageDirectoryPath}{oldImgFileName}");
                    }

                    var newImgFileName = $"{Guid.NewGuid()}{GlobalConstants.jpegFileExtension}";

                    this.DbContext.Articles.First(a => a.Id == id).MainPictureFileName = this.CommonService.UploadImageAsync(Constants.GlobalConstants.articlesImageDirectoryPath, newImgFileName, editArticleViewModel.MainPicture).Result;
                }
                
                this.DbContext.Articles.First(a => a.Id == id).EditedOn = DateTime.UtcNow;

                this.DbContext.SaveChanges();

            }
            
        }

        public void DeleteArticle(int id)
        {
            var article = this.DbContext.Articles.FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                var articleContentFileName = article.ArticleContentFileName;

                if (File.Exists($"{GlobalConstants.articlesContentDirectoryPath}{articleContentFileName}"))
                {
                    File.Delete($"{GlobalConstants.articlesContentDirectoryPath}{articleContentFileName}");
                }

                var articleImgFileName = article.MainPictureFileName;

                if (File.Exists($"{GlobalConstants.articlesImageDirectoryPath}{articleImgFileName}"))
                {
                    File.Delete($"{GlobalConstants.articlesImageDirectoryPath}{articleImgFileName}");
                }

                if (article.PicturesFileNames != null && article.PicturesFileNames.Count > 0)
                {
                    foreach (var picture in article.PicturesFileNames)
                    {
                        if (File.Exists($"{GlobalConstants.articlesImageDirectoryPath}{picture}"))
                        {
                            File.Delete($"{GlobalConstants.articlesImageDirectoryPath}{picture}");
                        }
                    }
                }

                this.DbContext.Articles.Remove(article);
                this.DbContext.SaveChanges();
            }
        }

        public List<Article> GetAllArticles()
        {
            var allArticles = this.DbContext.Articles.OrderByDescending(a => a.CreatedOn).ThenByDescending(a => a.EditedOn).ToList();

            return allArticles;
        }

        public Article GetArticle(int id)
        {
            var article = this.DbContext.Articles.FirstOrDefault(a => a.Id == id);

            return article;
        }

        private string UploadArticleContent(string articleContent)
        {
            var fileName = $"{Guid.NewGuid()}{GlobalConstants.textFileExtension}";

            var textFileName = this.CommonService.UploadTextToFileAsync(GlobalConstants.articlesContentDirectoryPath, fileName, articleContent).Result;

            return textFileName;
        }

        private void UploadArticleContent(string articleContent, string fileName)
        {
            var textFileName = this.CommonService.UploadTextToFileAsync(GlobalConstants.articlesContentDirectoryPath, fileName, articleContent).Result;
        }

        public string DownloadArticleContent(string fileName)
        {
            var articleContent = this.CommonService.DownloadTextFromFile(GlobalConstants.articlesContentDirectoryPath, fileName);

            return articleContent;
        }
    }
}
