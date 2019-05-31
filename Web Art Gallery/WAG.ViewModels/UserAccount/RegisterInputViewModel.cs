using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAG.ViewModels.UserAccount
{
    public class RegisterInputViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string City { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string UnsuccessfulRegistrationMessage { get; set; }
    }
}
