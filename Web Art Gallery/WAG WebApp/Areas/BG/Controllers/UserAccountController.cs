using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WAG.Services.Interfaces;
using WAG.ViewModels.UserAccount;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WAG.WebApp.Areas.BG.Controllers
{
    public class UserAccountController : BGController
    {
        private const string UnsuccessfulRegistrationMessage = "This Username already exist! Please choose another one";
        private const string UnsuccessfulLogInMessage = "Username or password is incorrect!";

        private IUserAccountService UserAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            this.UserAccountService = userAccountService;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginInputViewModel loginInputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(loginInputModel);
            }

            var loginResult = this.UserAccountService.LoginUserSuccessfully(loginInputModel);

            if (loginResult != SignInResult.Success)
            {
                loginInputModel.UnsuccessfulLogInMessage = UnsuccessfulLogInMessage;

                return this.View(loginInputModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputViewModel registerInputViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(registerInputViewModel);
            }

            var registerResult = this.UserAccountService.CreateUserAsync(registerInputViewModel).Result;

            if (registerResult != IdentityResult.Success)
            {
                registerInputViewModel.UnsuccessfulRegistrationMessage = UnsuccessfulRegistrationMessage;

                return this.View(registerInputViewModel);
            }

            return RedirectToAction("Login", "UserAccount");
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.UserAccountService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyProfile(UserDetailsViewModel userDetailsViewModel)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            userDetailsViewModel.UserName = currUser.UserName;
            userDetailsViewModel.FirstName = currUser.FirstName;
            userDetailsViewModel.LastName = currUser.LastName;
            userDetailsViewModel.City = currUser.City;
            userDetailsViewModel.Address = currUser.Address;
            userDetailsViewModel.Email = currUser.Email;
            userDetailsViewModel.PhoneNumber = currUser.PhoneNumber;
            userDetailsViewModel.IdentityRoles = this.UserAccountService.GetUserRolesNameById(currUser.Id).ToList();

            return this.View(userDetailsViewModel);
        }

        [Authorize]
        public IActionResult EditUserProfile(EditUserProfileViewModel editUserProfileViewModel)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            editUserProfileViewModel.FirstName = currUser.FirstName;
            editUserProfileViewModel.LastName = currUser.LastName;
            editUserProfileViewModel.City = currUser.City;
            editUserProfileViewModel.Address = currUser.Address;
            editUserProfileViewModel.Email = currUser.Email;
            editUserProfileViewModel.PhoneNumber = currUser.PhoneNumber;

            return this.View(editUserProfileViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditUserProfile(EditUserProfileInputViewModel editUserProfileInputViewModel)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            this.UserAccountService.EditUserProfile(currUser, editUserProfileInputViewModel);

            return RedirectToAction("Success", "Home");
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(changePasswordViewModel);
            }

            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            this.UserAccountService.ChangePassword(currUser, changePasswordViewModel.CurrPassword, changePasswordViewModel.NewPassword);
            
            return RedirectToAction("Success", "Home");
        }
    }
}
