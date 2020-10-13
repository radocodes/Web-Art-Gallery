﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Home;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class HomeController : AdministrationController
    {
        private IHomeService HomeService;

        public HomeController(IHomeService homeService, IMapper mapper)
            : base(mapper)
        {
            this.HomeService = homeService;
        }

        public IActionResult Messages(MessagesViewModel messagesViewModel)
        {
            messagesViewModel.Messages = this.HomeService.GetAllMessages();

            var MessageMaxLengthInTable = 100;

            foreach (var message in messagesViewModel.Messages)
            {
                if (message.Title != null && message.Title.Length > MessageMaxLengthInTable)
                {
                    message.Title = message.Title.Substring(0, MessageMaxLengthInTable);
                }
            }

            return this.View(messagesViewModel);
        }

        public IActionResult MessageDetails(int id, MessageDetailsViewModel messageDetailsViewModel)
        {
            messageDetailsViewModel.Message = this.HomeService.GetContactMessageById(id);

            if (messageDetailsViewModel.Message == null)
            {
                return RedirectToAction("Messages", "Home", new { area = "Administration" });
            }

            return this.View(messageDetailsViewModel);
        }
        
        [HttpPost]
        public IActionResult DeleteContactMessage(int id)
        {
            if (this.HomeService.GetContactMessageById(id) == null)
            {
                return RedirectToAction("Messages", "Home", new { area = "Administration" });
            }

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