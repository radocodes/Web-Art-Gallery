using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.ViewModels.Order
{
    public class MakeOrderViewModel
    {
        public int ArtWorkId { get; set; }

        public string OrderInfo { get; set; }

        public string TelephoneNumberForContact { get; set; }

        public string DeliveryAddress { get; set; }
    }
}
