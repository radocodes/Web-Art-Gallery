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

        public BlogController(IBlogService blogService, ICommonService commonService)
        {
            this.BlogService = blogService;
            this.CommonService = commonService;
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

            return this.View(articleDetailsViewModel);
        }
    }
}