using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService;
        private ICloudinaryService cloudinaryService;
        private ICommentService CommentService;

        public BlogController(IBlogService blogService, ICloudinaryService cloudinaryService, ICommentService commentService)
        {
            this.BlogService = blogService;
            this.cloudinaryService = cloudinaryService;
            this.CommentService = commentService;
        }

        public IActionResult Index(BlogIndexViewModel viewModel)
        {
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
            var article = this.BlogService.GetArticle(id);

            if (article == null)
            {
                return RedirectToAction("Index", "Blog");
            }

            var viewModel = new ArticleDetailsViewModel()
            {
                ArticleId = article.Id,
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                ArticleContent = article.ArticleContent,
                CreatedOn = article.CreatedOn,
                EditedOn = article.EditedOn,
                MainPictureFileName = article.MainPictureFileName,
                Comments = this.CommentService.GetArticleComments(article.Id),

                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            return this.View(viewModel);
        }
    }
}