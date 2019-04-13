using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WAG.Data.Models
{
    public class Picture
    {
        public Picture()    
        {
            this.ArticlePictures = new HashSet<ArticlePicture>();
            this.ArtEventPictures = new HashSet<ArtEventPicture>();
        }

        //[ForeignKey("ArtisticWork")]
        public int Id { get; set; }

        public string URL { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<ArticlePicture> ArticlePictures { get; set; }

        public virtual ICollection<ArtEventPicture> ArtEventPictures { get; set; }
    }
}
