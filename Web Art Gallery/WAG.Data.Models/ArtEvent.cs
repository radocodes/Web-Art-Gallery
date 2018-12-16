using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class ArtEvent : BaseModel<int>
    {
        public ArtEvent()
        {
            this.ArtEventPictures = new HashSet<ArtEventPicture>();
        }

        public string Info { get; set; }

        public DateTime EventDate { get; set; }

        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ArtEventPicture> ArtEventPictures { get; set; }
    }
}
