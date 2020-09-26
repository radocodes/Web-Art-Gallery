using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService;
        private IFileService CommonService;
        private ICommentService CommentService;

        public BlogController(IBlogService blogService, IFileService commonService, ICommentService commentService)
        {
            this.BlogService = blogService;
            this.CommonService = commonService;
            this.CommentService = commentService;
        }

        public IActionResult Index(BlogIndexViewModel blogIndexViewModel)
        {
            blogIndexViewModel.AllArticles = this.BlogService.GetAllArticles();

            foreach (var article in blogIndexViewModel.AllArticles)
            {
                article.Comments = this.CommentService.GetArticleComments(article.Id);
            }

            return this.View(blogIndexViewModel);
        }

        public IActionResult ArticleDetails(int id, ArticleDetailsViewModel articleDetailsViewModel)
        {
            var article = this.BlogService.GetArticle(id);

            if (article == null)
            {
                return RedirectToAction("Index", "Blog");
            }

            articleDetailsViewModel.ArticleId = article.Id;
            articleDetailsViewModel.Title = article.Title;
            articleDetailsViewModel.ShortDescription = article.ShortDescription;
            articleDetailsViewModel.ArticleContent = article.ArticleContent;
            articleDetailsViewModel.CreatedOn = article.CreatedOn;
            articleDetailsViewModel.EditedOn = article.EditedOn;
            articleDetailsViewModel.MainPictureFileName = article.MainPictureFileName;
            articleDetailsViewModel.Comments = this.CommentService.GetArticleComments(article.Id);

            return this.View(articleDetailsViewModel);
        }
    }
}