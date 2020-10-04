﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Home;
using WAG_WebApp.Models;

namespace WAG.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IHomeService HomeService;
        private IUserAccountService UserAccountService;

        public HomeController(IHomeService homeService, IUserAccountService userAccountService)
        {
            this.HomeService = homeService;
            this.UserAccountService = userAccountService;
        }

        //IndexStart() exists to load start page from "BG" area, but only first time (when access application URL from the begining).
        //Beacause I did't find better routing approach (Temporary solution).
        public IActionResult IndexStart()
        {
            return RedirectToAction("Index", "Home", new { area = "BG" });
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("IndexLogged", "Home");
            }

            return View();
        }

        [Authorize]
        public IActionResult IndexLogged()
        {
            return View();
        }

        public IActionResult About(AboutViewModel aboutViewModel)
        {
            aboutViewModel.Biography = this.HomeService.GetBiography();

            return View(aboutViewModel);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel contactMessageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(contactMessageViewModel);
            }
            string userId = null;

            if (User.Identity.IsAuthenticated)
            {
                userId = this.UserAccountService.GetCurrentUser(HttpContext).Id;
            }

            this.HomeService.SaveContactMessage(contactMessageViewModel, userId);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
