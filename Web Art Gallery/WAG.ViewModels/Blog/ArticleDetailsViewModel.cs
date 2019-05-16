using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.ViewModels.Blog
{
    public class ArticleDetailsViewModel
    {
        public Article Article { get; set; }

        public string ArticleContent { get; set; }

        public List<WAG.Data.Models.Comment> Comments { get; set; }
    }
}
