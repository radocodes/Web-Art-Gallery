using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Blog;

namespace WAG.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService;
        private ICommonService CommonService;
        private ICommentService CommentService;

        public BlogController(IBlogService blogService, ICommonService commonService, ICommentService commentService)
        {
            this.BlogService = blogService;
            this.CommonService = commonService;
            this.CommentService = commentService;
        }

        public IActionResult Index(BlogIndexViewModel blogIndexViewModel)
        {
            blogIndexViewModel.AllArticles = this.BlogService.GetAllArticles();

            return this.View(blogIndexViewModel);
        }

        public IActionResult ArticleDetails(int id, ArticleDetailsViewModel articleDetailsViewModel)
        {
            var article = this.BlogService.GetArticle(id);

            articleDetailsViewModel.Article = article;

            articleDetailsViewModel.ArticleContent = this.BlogService.DownloadArticleContent(article.Description);

            articleDetailsViewModel.Comments = this.CommentService.GetArticleComments(article.Id);

            return this.View(articleDetailsViewModel);
        }
    }
}