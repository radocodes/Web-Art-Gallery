using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WAG.ViewModels.Blog
{
    public class CreateArticleViewModel
    {
        [Required]
        public string Title { get; set; }

        [Display(Name = "Short Description")]
        [Required]
        public string ShortDescription { get; set; }

        [Display(Name = "Article Content")]
        [Required]
        public string ArticleContent { get; set; }

        public string MainPictureFileName { get; set; }

        public ICollection<string> Pictures { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
