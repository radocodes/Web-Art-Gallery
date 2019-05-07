using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.ViewModels.Blog
{
    public class CreateArticleViewModel
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public IFormFile MainPicture { get; set; }

        public ICollection<IFormFile> Pictures { get; set; }
    }
}
