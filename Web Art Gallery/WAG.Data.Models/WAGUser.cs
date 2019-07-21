using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace WAG.Data.Models
{
    public class WAGUser : IdentityUser
    {
        public WAGUser()
        {
            this.Orders = new HashSet<Order>();
            this.Comments = new HashSet<Comment>();
            this.Articles = new HashSet<Article>();
            this.ContactMessages = new HashSet<ContactMessage>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<ContactMessage> ContactMessages { get; set; }
    }
}
