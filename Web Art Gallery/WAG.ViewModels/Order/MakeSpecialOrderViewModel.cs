﻿using System.ComponentModel.DataAnnotations;

namespace WAG.ViewModels.Order
{
    public class MakeSpecialOrderViewModel
    {
        [Display(Name = "Order Info")]
        [Required]
        public string OrderInfo { get; set; }

        [Display(Name = "Telephone Number for Contact")]
        public string TelephoneNumberForContact { get; set; }

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }
    }
}
