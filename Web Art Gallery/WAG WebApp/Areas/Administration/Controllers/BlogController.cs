using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class BlogController : AdministrationController
    {
        private readonly IBlogService BlogService;
        private readonly ICloudinaryService cloudinaryService;

        public BlogController(IBlogService blogService, ICloudinaryService cloudinaryService, IMapper mapper)
            : base(mapper)
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

            Article articleModel = mapper.Map<Article>(viewModel);

            this.BlogService.CreateArticle(articleModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArticle(int id)
        {
            var articleToEdit = this.BlogService.GetArticle(id);

            if (articleToEdit == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            EditArticleViewModel viewModel = mapper.Map<EditArticleViewModel>(articleToEdit);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditArticle(EditArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            Article articleToEdit = mapper.Map<Article>(viewModel);
            this.BlogService.EditArticle(articleToEdit);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult DeleteArticle(int id)
        {

            Article ArticleToDelete = this.BlogService.GetArticle(id);

            if (ArticleToDelete == null)
            {
                return RedirectToAction("Index", "Blog", new { area = "" });
            }

            DeleteArticleViewModel viewModel = mapper.Map<DeleteArticleViewModel>(ArticleToDelete);
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