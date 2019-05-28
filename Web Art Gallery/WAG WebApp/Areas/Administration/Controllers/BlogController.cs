using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class BlogController : AdministrationController
    {
        private IBlogService BlogService;

        public BlogController(IBlogService blogService)
        {
            this.BlogService = blogService;
        }

        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticleViewModel createArticleViewModel)
        {
            this.BlogService.CreateArticle(createArticleViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArticle(int id, EditArticleViewModel editArticleViewModel)
        {
            var articleToEdit = this.BlogService.GetArticle(id);

            editArticleViewModel.Title = articleToEdit.Title;
            editArticleViewModel.ShortDescription = articleToEdit.ShortDescription;
            editArticleViewModel.ArticleContent = this.BlogService.DownloadArticleContent(articleToEdit.ArticleContentFileName);

            return this.View(editArticleViewModel);
        }

        [HttpPost]
        public IActionResult EditArticle(int id, EditArticleInputViewModel editArticleInputViewModel)
        {
            this.BlogService.EditArticle(id, editArticleInputViewModel);
            
            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult DeleteArticle(int id, DeleteArticleViewModel deleteArticleViewModel)
        {
            deleteArticleViewModel.Article = this.BlogService.GetArticle(id);

            return this.View(deleteArticleViewModel);
        }

        [HttpPost]
        public IActionResult DeleteArticle(int id)
        {
            this.BlogService.DeleteArticle(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult ManageArticles()
        {
            return this.View();
        }
    }
}