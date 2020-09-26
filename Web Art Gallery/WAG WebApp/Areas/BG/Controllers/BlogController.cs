using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Areas.BG.Controllers
{
    public class BlogController : BGController
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

            articleDetailsViewModel.Article = article;

            articleDetailsViewModel.ArticleContent = this.BlogService.DownloadArticleContent(article.ArticleContent);

            articleDetailsViewModel.Comments = this.CommentService.GetArticleComments(article.Id);

            return this.View(articleDetailsViewModel);
        }
    }
}