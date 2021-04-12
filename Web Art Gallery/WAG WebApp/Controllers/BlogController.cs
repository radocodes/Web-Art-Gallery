using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;
using WAG.WebApp.Controllers.Common;

namespace WAG.WebApp.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService BlogService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICommentService CommentService;

        public BlogController(IBlogService blogService, ICloudinaryService cloudinaryService, ICommentService commentService, IMapper mapper)
            : base(mapper)
        {
            this.BlogService = blogService;
            this.cloudinaryService = cloudinaryService;
            this.CommentService = commentService;
        }

        public IActionResult Index()
        {
            var viewModel = new BlogIndexViewModel();
            viewModel.AllArticles = this.BlogService.GetAllArticles();

            foreach (var article in viewModel.AllArticles)
            {
                article.Comments = this.CommentService.GetArticleComments(article.Id);
            }

            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }

        public IActionResult ArticleDetails(int id)
        {
            Article article = this.BlogService.GetArticle(id);

            if (article == null)
            {
                return RedirectToAction("Index", "Blog");
            }

            ArticleDetailsViewModel viewModel = mapper.Map<ArticleDetailsViewModel>(article);
            viewModel.Comments = this.CommentService.GetArticleComments(article.Id);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }
    }
}