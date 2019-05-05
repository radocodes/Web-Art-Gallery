using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WAG.Data.Models
{
    public class ArtEvent : BaseModel<int>
    {
        public ArtEvent()
        {
            this.Pictures = new HashSet<string>();
        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime EditedOn { get; set; }

        public string MainPicture { get; set; }

        [NotMapped]
        public ICollection<string> Pictures { get; set; }
    }
}
