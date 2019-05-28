using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class ContactMessage : BaseModel<int>
    {
        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public string Title { get; set; }

        public string TextBody { get; set; }

        public DateTime Date { get; set; }

        public bool Read { get; set; }
    }
}
