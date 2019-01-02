using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAG.ViewModels.InputViewModels;

namespace WAG.WebApp.Controllers
{
    public class UserAccountController : Controller
    {
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginInputViewModel loginInputModel)
        {
            return this.View();
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputViewModel registerInputViewModel)
        {
            return this.View();
        }

        public IActionResult Logout()
        {
            //TODO: improve to work correct
            return RedirectToAction("Index", "Home");
        }
    }
}
