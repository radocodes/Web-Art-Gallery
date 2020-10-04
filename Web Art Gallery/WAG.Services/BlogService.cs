﻿using System;
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
            articleNew.ArticleContent = createArticleViewModel.ArticleContent;
            articleNew.MainPictureFileName = createArticleViewModel.MainPictureFileName;
            articleNew.CreatedOn = DateTime.UtcNow;

            this.DbContext.Articles.Add(articleNew);
            this.DbContext.SaveChanges();
        }

        public void EditArticle(EditArticleViewModel editArticleViewModel)
        {
            var articleToUpdate = this.DbContext.Articles.FirstOrDefault(a => a.Id == editArticleViewModel.ArticleId);

            if (articleToUpdate != null)
            {
                articleToUpdate.Title = editArticleViewModel.Title;
                articleToUpdate.ShortDescription = editArticleViewModel.ShortDescription;
                articleToUpdate.ArticleContent = editArticleViewModel.ArticleContent;
                articleToUpdate.EditedOn = DateTime.UtcNow;
                articleToUpdate.MainPictureFileName = editArticleViewModel.MainPictureFileName;

                this.DbContext.Articles.Update(articleToUpdate);
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
            if (File.Exists($"{GlobalConstants.ArticlesContentDirectoryPath}{fileName}"))
            {
                var articleContent = this.FileService.DownloadTextFromFile(GlobalConstants.ArticlesContentDirectoryPath, fileName);

                return articleContent;
            }
            else
            {
                return null;
            }
        }
    }
}
