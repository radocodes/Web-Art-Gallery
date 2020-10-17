using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.Blog
{
    public class DeleteArticleViewModel
    {
        public string ArticleId { get; set; }

        public string ArticleTitle { get; set; }

        public string MainPictureFileName { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
