using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class BlogController : AdministrationController
    {
        private IBlogService BlogService;
        private ICloudinaryService cloudinaryService;

        public BlogController(IBlogService blogService, ICloudinaryService cloudinaryService)
        {
            this.BlogService = blogService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult CreateArticle()
        {
            var viewModel = new CreateArticleViewModel()
            {
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            this.BlogService.CreateArticle(viewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArticle(int id)
        {
            var articleToEdit = this.BlogService.GetArticle(id);

            if (articleToEdit == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            var viewModel = new EditArticleViewModel
            {
                ArticleId = articleToEdit.Id,
                Title = articleToEdit.Title,
                ShortDescription = articleToEdit.ShortDescription,
                ArticleContent = articleToEdit.ArticleContent,

                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditArticle(EditArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }
            
            if (this.BlogService.GetArticle(viewModel.ArticleId) == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            this.BlogService.EditArticle(viewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult DeleteArticle(int id)
        {
            var viewModel = new DeleteArticleViewModel()
            {
                Article = this.BlogService.GetArticle(id)
            };

            if (viewModel.Article == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteArticlePost(int id)
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