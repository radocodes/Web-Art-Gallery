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
            if (!ModelState.IsValid)
            {
                return this.View(createArticleViewModel);
            }

            this.BlogService.CreateArticle(createArticleViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArticle(int id)
        {
            var articleToEdit = this.BlogService.GetArticle(id);

            if (articleToEdit == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            var editArticleViewModel = new EditArticleViewModel
            {
                Title = articleToEdit.Title,
                ShortDescription = articleToEdit.ShortDescription,
                ArticleContent = articleToEdit.ArticleContent,
            };

            return this.View(editArticleViewModel);
        }

        [HttpPost]
        public IActionResult EditArticle(int id, EditArticleViewModel editArticleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(editArticleViewModel);
            }
            
            if (this.BlogService.GetArticle(id) == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            this.BlogService.EditArticle(id, editArticleViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult DeleteArticle(int id, DeleteArticleViewModel deleteArticleViewModel)
        {
            deleteArticleViewModel.Article = this.BlogService.GetArticle(id);

            if (deleteArticleViewModel.Article == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            return this.View(deleteArticleViewModel);
        }

        [HttpPost]
        public IActionResult DeleteArticle(int id)
        {
            if (this.BlogService.GetArticle(id) == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }
            
            this.BlogService.DeleteArticle(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult ManageArticles()
        {
            return this.View();
        }
    }
}