using System.Collections.Generic;
using WAG.Data.Models;
using WAG.ViewModels.Blog;

namespace WAG.Services.Interfaces
{
    public interface IBlogService
    {
        void CreateArticle(CreateArticleViewModel createArticleViewModel);

        void EditArticle(int id, EditArticleViewModel editArticleInputViewModel);

        void DeleteArticle(int id);

        List<Article> GetAllArticles();

        Article GetArticle(int id);

        string DownloadArticleContent(string fileName);
    }
}
