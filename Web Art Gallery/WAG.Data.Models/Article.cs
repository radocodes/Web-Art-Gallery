using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class Article : BaseModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.ArticlePictures = new HashSet<ArticlePicture>();
        }

        public string TextBody { get; set; }

        public DateTime CreatedOn { get; set; }

        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ArticlePicture> ArticlePictures { get; set; }
    }
}
