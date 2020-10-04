using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.Blog
{
    public class DeleteArticleViewModel
    {
        public Article Article { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
