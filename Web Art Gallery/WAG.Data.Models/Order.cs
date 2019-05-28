using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class Order : BaseModel<int>
    {
        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public int? ArtisticWorkId { get; set; }

        public virtual ArtisticWork ArtisticWork { get; set; }

        public string OrderInfo { get; set; }

        public string TelephoneNumberForContact { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
