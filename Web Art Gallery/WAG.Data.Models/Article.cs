using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WAG.Data.Models
{
    public class Article : BaseModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.PicturesFileNames = new HashSet<string>();
        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ArticleContentFileName { get; set; }

        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime EditedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string MainPictureFileName { get; set; }

        [NotMapped]
        public ICollection<string> PicturesFileNames { get; set; }
    }
}
