using System.ComponentModel.DataAnnotations;

namespace WAG.ViewModels.UserAccount
{
    public class LoginInputViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string UnsuccessfulLogInMessage { get; set; }
    }
}
