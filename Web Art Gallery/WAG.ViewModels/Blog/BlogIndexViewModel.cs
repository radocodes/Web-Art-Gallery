using CloudinaryDotNet;
using System.Collections.Generic;
using WAG.Data.Models;

namespace WAG.ViewModels.Blog
{
    public class BlogIndexViewModel
    {
        public List<Article> AllArticles { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
