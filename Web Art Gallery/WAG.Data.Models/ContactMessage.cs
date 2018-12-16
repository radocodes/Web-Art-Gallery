using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class ContactMessage : BaseModel<int>
    {
        public int WAGUserId { get; set; }

        public virtual string WAGUser { get; set; }

        public string Title { get; set; }

        public string TextBody { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }
}
