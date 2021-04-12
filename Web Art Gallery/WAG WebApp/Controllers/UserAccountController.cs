using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WAG.Data.Models;
using WAG.Services.Constants;
using WAG.Services.Interfaces;
using WAG.ViewModels.UserAccount;
using WAG.WebApp.Controllers.Common;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WAG.WebApp.Controllers
{
    public class UserAccountController : BaseController
    {
        private const string UnsuccessfulRegistrationMessage = "This Username already exist! Please choose another one";
        private const string UnsuccessfulLogInMessage = "Username or password is incorrect!";

        private readonly IUserAccountService UserAccountService;

        public UserAccountController(IUserAccountService userAccountService, IMapper mapper)
            : base(mapper)
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

            if (loginResult.Succeeded)
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

            WAGUser userNew = mapper.Map<WAGUser>(registerInputViewModel);
            var registerResult = this.UserAccountService.CreateUserAsync(userNew, registerInputViewModel.Password).Result;

            if (registerResult.Succeeded)
            {
                registerInputViewModel.UnsuccessfulRegistrationMessage = UnsuccessfulRegistrationMessage;

                return this.View(registerInputViewModel);
            }

            var currUser = this.UserAccountService.GetUserByUserName(registerInputViewModel.UserName);

            this.UserAccountService.AddUserInRoleAsync(currUser, GlobalConstants.UserRole).GetAwaiter().GetResult();

            return RedirectToAction("Login", "UserAccount");
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.UserAccountService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            UserDetailsViewModel viewModel = mapper.Map<UserDetailsViewModel>(currUser);
            viewModel.IdentityRoles = this.UserAccountService.GetUserRolesNameById(currUser.Id).ToList();

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult EditUserProfile()
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            EditUserProfileViewModel viewModel = mapper.Map<EditUserProfileViewModel>(currUser);
            return this.View(viewModel);
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
