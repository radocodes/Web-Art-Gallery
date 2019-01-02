using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAG.Data.Models;
using WAG.Services;
using WAG.Services.Interfaces;
using WAG.ViewModels.InputViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WAG.WebApp.Controllers
{
    public class UserAccountController : Controller
    {
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
            var loginResult = this.UserAccountService.LoginUserSuccessfully(loginInputModel);

            if (loginResult != SignInResult.Success)
            {
                //TODO: error message
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
            var registerResult = this.UserAccountService.RegisterUserSuccessfully(registerInputViewModel).Result;

            if (registerResult != SignInResult.Success)
            {
                //TODO: error message
                return this.View(registerInputViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            this.UserAccountService.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
