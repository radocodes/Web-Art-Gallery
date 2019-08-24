using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private IFileService FileService;

        public BlogService(WAGDbContext dbContext, IFileService fileService)
        {
            this.DbContext = dbContext;
            this.FileService = fileService;
        }
        
        public void CreateArticle(CreateArticleViewModel createArticleViewModel)
        {
            var articleNew = new Article();

            articleNew.Title = createArticleViewModel.Title;
            articleNew.ShortDescription = createArticleViewModel.ShortDescription;
            articleNew.ArticleContentFileName = this.UploadArticleContent(createArticleViewModel.ArticleContent);

            if (createArticleViewModel.MainPicture != null)
            {
                var imgFileName = $"{Guid.NewGuid()}{GlobalConstants.JpegFileExtension}";

                articleNew.MainPictureFileName = this.FileService.UploadImageAsync(GlobalConstants.ArticlesImageDirectoryPath, imgFileName, createArticleViewModel.MainPicture).Result;
            }
            
            articleNew.CreatedOn = DateTime.UtcNow;

            this.DbContext.Articles.Add(articleNew);
            this.DbContext.SaveChanges();
        }

        public void EditArticle(int id, EditArticleViewModel editArticleViewModel)
        {
            var articleToUpdate = this.DbContext.Articles.FirstOrDefault(a => a.Id == id);

            if (articleToUpdate != null)
            {
                articleToUpdate.Title = editArticleViewModel.Title;
                articleToUpdate.ShortDescription = editArticleViewModel.ShortDescription;
                this.UploadArticleContent(editArticleViewModel.ArticleContent, articleToUpdate.ArticleContentFileName);

                if (editArticleViewModel.MainPicture != null)
                {
                    var oldImgFileName = this.DbContext.Articles.First(a => a.Id == id).MainPictureFileName;

                    if (File.Exists($"{GlobalConstants.ArticlesImageDirectoryPath}{oldImgFileName}"))
                    {
                        File.Delete($"{GlobalConstants.ArticlesImageDirectoryPath}{oldImgFileName}");
                    }

                    var newImgFileName = $"{Guid.NewGuid()}{GlobalConstants.JpegFileExtension}";

                    this.DbContext.Articles.First(a => a.Id == id).MainPictureFileName = this.FileService.UploadImageAsync(Constants.GlobalConstants.ArticlesImageDirectoryPath, newImgFileName, editArticleViewModel.MainPicture).Result;
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

                if (File.Exists($"{GlobalConstants.ArticlesContentDirectoryPath}{articleContentFileName}"))
                {
                    File.Delete($"{GlobalConstants.ArticlesContentDirectoryPath}{articleContentFileName}");
                }

                var articleImgFileName = article.MainPictureFileName;

                if (File.Exists($"{GlobalConstants.ArticlesImageDirectoryPath}{articleImgFileName}"))
                {
                    File.Delete($"{GlobalConstants.ArticlesImageDirectoryPath}{articleImgFileName}");
                }

                if (article.PicturesFileNames != null && article.PicturesFileNames.Count > 0)
                {
                    foreach (var picture in article.PicturesFileNames)
                    {
                        if (File.Exists($"{GlobalConstants.ArticlesImageDirectoryPath}{picture}"))
                        {
                            File.Delete($"{GlobalConstants.ArticlesImageDirectoryPath}{picture}");
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
            var fileName = $"{Guid.NewGuid()}{GlobalConstants.TextFileExtension}";

            var textFileName = this.FileService.UploadTextToFileAsync(GlobalConstants.ArticlesContentDirectoryPath, fileName, articleContent).Result;

            return textFileName;
        }

        private void UploadArticleContent(string articleContent, string fileName)
        {
            var textFileName = this.FileService.UploadTextToFileAsync(GlobalConstants.ArticlesContentDirectoryPath, fileName, articleContent).Result;
        }

        public string DownloadArticleContent(string fileName)
        {
            var articleContent = this.FileService.DownloadTextFromFile(GlobalConstants.ArticlesContentDirectoryPath, fileName);

            return articleContent;
        }
    }
}
