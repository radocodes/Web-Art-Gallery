using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.Services
{
    public class BlogService : IBlogService
    {
        private WAGDbContext DbContext;
        private ICommonService CommonService;

        public BlogService(WAGDbContext dbContext, ICommonService commonService)
        {
            this.DbContext = dbContext;
            this.CommonService = commonService;
        }
        
        public void CreateArticle(CreateArticleViewModel createArticleViewModel)
        {
            var articleNew = new Article()
            {
                Title = createArticleViewModel.Title,
                ShortDescription = createArticleViewModel.ShortDescription,
                Description = this.UploadArticleContent(createArticleViewModel.Description),
                MainPicture = this.CommonService.UploadPictureAsync(createArticleViewModel.MainPicture).Result,
                CreatedOn = DateTime.UtcNow,
            };

            this.DbContext.Articles.Add(articleNew);

            this.DbContext.SaveChanges();
        }

        public void EditArticle(int id, EditArticleInputViewModel editArticleInputViewModel)
        {
            if (this.DbContext.Articles.Any(a => a.Id == id))
            {
                this.DbContext.Articles.First(a => a.Id == id).Title = editArticleInputViewModel.Title;
                this.DbContext.Articles.First(a => a.Id == id).ShortDescription = editArticleInputViewModel.ShortDescription;
                this.DbContext.Articles.First(a => a.Id == id).Description = this.UploadArticleContent(editArticleInputViewModel.Description);

                if (editArticleInputViewModel.MainPicture != null)
                {
                    this.DbContext.Articles.First(a => a.Id == id).MainPicture = this.CommonService.UploadPictureAsync(editArticleInputViewModel.MainPicture).Result;
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
                this.DbContext.Articles.Remove(article);
                this.DbContext.SaveChanges();
            }
        }

        public List<Article> GetAllArticles()
        {
            var allArticles = this.DbContext.Articles.ToList();

            return allArticles;
        }

        public Article GetArticle(int id)
        {
            var article = this.DbContext.Articles.FirstOrDefault(a => a.Id == id);

            return article;
        }

        public string UploadArticleContent(string articleContentent)
        {
            var directoryPath = @"D:\RADO\IT\Projects\Web Art Gallery\Web Art Gallery\WAG WebApp\wwwroot\articles\";

            var textFileName = this.CommonService.UploadTextToFileAsync(articleContentent, directoryPath).Result;

            return textFileName;
        }

        public string DownloadArticleContent(string fileName)
        {
            var directoryPath = @"D:\RADO\IT\Projects\Web Art Gallery\Web Art Gallery\WAG WebApp\wwwroot\articles\";

            var articleContent = this.CommonService.DownloadTextFromFile(fileName, directoryPath);

            return articleContent;
        }
    }
}
