using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.Home;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class HomeController : AdministrationController
    {
        private readonly IContactMessageService HomeService;
        private readonly IBiographyService biographyService;

        public HomeController(IContactMessageService homeService, IBiographyService biographyService, IMapper mapper)
            : base(mapper)
        {
            this.HomeService = homeService;
            this.biographyService = biographyService;
        }

        public IActionResult Messages(MessagesViewModel messagesViewModel)
        {
            messagesViewModel.Messages = this.HomeService.GetAllContactMessages();

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

        public IActionResult EditBiography()
        {
            var editBiographyViewModel = new EditBiographyViewModel()
            {
                Biography = this.biographyService.GetBiography()
            };

            return this.View(editBiographyViewModel);
        }

        [HttpPost]
        public IActionResult UpdateBiography(EditBiographyViewModel viewModel)
        {
            this.biographyService.EditBiography(viewModel.Biography);

            return RedirectToAction("Success", "Home", new { area = "" });
        }
    }
}