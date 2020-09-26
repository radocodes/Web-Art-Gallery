using System;
using System.Collections.Generic;
using WAG.Data.Models;

namespace WAG.ViewModels.Blog
{
    public class ArticleDetailsViewModel
    {
        public ArticleDetailsViewModel()
        {
            this.Comments = new HashSet<WAG.Data.Models.Comment>();
        }
        
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ArticleContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime EditedOn { get; set; }

        public virtual ICollection<WAG.Data.Models.Comment> Comments { get; set; }

        public string MainPictureFileName { get; set; }
    }
}
