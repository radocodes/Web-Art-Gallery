using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAG.ViewModels.UserAccount
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Current Password")]
        [Required]
        public string CurrPassword { get; set; }

        [Display(Name = "New Password")]
        [Required]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}
