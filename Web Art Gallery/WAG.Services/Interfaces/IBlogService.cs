using System.Collections.Generic;
using WAG.Data.Models;
using WAG.ViewModels.Blog;

namespace WAG.Services.Interfaces
{
    public interface IBlogService
    {
        void CreateArticle(Article articleNew);

        void EditArticle(Article articleToUpdate);

        void DeleteArticle(int id);

        List<Article> GetAllArticles();

        Article GetArticle(int id);

        string DownloadArticleContent(string fileName);
    }
}
