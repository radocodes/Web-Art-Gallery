using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.ArtisticWorks = new HashSet<ArtisticWork>();
        }

        public int WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public bool IsDeleted { get; set; }

        public string OrderInfo { get; set; }

        public string TelephoneNumberForContact { get; set; }

        public string DeliveryAddress { get; set; }

        public virtual ICollection<ArtisticWork> ArtisticWorks { get; set; }
    }
}
