using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.Home;
using WAG.WebApp.Controllers.Common;
using WAG_WebApp.Models;

namespace WAG.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private IContactMessageService HomeService;
        private IUserAccountService UserAccountService;
        private IBiographyService biographyService;

        public HomeController(IContactMessageService homeService, IUserAccountService userAccountService, IBiographyService biographyService, IMapper mapper)
            : base(mapper)
        {
            this.HomeService = homeService;
            this.UserAccountService = userAccountService;
            this.biographyService = biographyService;
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

        public IActionResult About()
        {
            var aboutViewModel = new AboutViewModel()
            {
                Biography = this.biographyService.GetBiography()
            };

            return View(aboutViewModel);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            string userId = null;

            if (User.Identity.IsAuthenticated)
            {
                userId = this.UserAccountService.GetCurrentUser(HttpContext).Id;
            }

            ContactMessage contactMessage = mapper.Map<ContactMessage>(viewModel);
            this.HomeService.SaveContactMessage(contactMessage, userId);

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
