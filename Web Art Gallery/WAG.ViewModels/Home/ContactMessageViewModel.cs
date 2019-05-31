using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAG.ViewModels.Home
{
    public class ContactMessageViewModel
    {
        [Required]
        public string Title { get; set; }

        [Display(Name = "Message")]
        [Required]
        public string TextBody { get; set; }
    }
}
