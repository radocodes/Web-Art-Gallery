using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Home;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class HomeController : AdministrationController
    {
        private IHomeService HomeService;

        public HomeController(IHomeService homeService)
        {
            this.HomeService = homeService;
        }


        public IActionResult Messages(MessagesViewModel messagesViewModel)
        {
            messagesViewModel.Messages = this.HomeService.GetAllMessages();

            foreach (var message in messagesViewModel.Messages)
            {
                if (message.Title.Length > 50)
                {
                    message.Title = message.Title.Substring(0, 100);
                }
            }

            return this.View(messagesViewModel);
        }

        public IActionResult MessageDetails(int id, MessageDetailsViewModel messageDetailsViewModel)
        {
            messageDetailsViewModel.Message = this.HomeService.GetContactMessageById(id);

            return this.View(messageDetailsViewModel);
        }
        
        [HttpPost]
        public IActionResult DeleteContactMessage(int id)
        {
            this.HomeService.DeleteContactMessage(id);

            return RedirectToAction("Messages", "Home", new { area = "Administration" });
        }

        public IActionResult EditBiography(EditBiographyViewModel editBiographyViewModel)
        {
            editBiographyViewModel.Biography = this.HomeService.GetBiography();

            return this.View(editBiographyViewModel);
        }

        [HttpPost]
        public IActionResult UpdateBiography(EditBiographyViewModel editBiographyViewModel)
        {
            this.HomeService.EditBiography(editBiographyViewModel.Biography);

            return RedirectToAction("Success", "Home", new { area = "" });
        }
    }
}