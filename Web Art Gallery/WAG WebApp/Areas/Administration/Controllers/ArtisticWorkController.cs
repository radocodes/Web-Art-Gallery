using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.InputViewModels;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class ArtisticWorkController : AdministrationController
    {
        private IArtisticWorkService ArtisticWorkService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService)
        {
            this.ArtisticWorkService = artisticWorkService;
        }

        public IActionResult ManageArtWorks()
        {
            return View();
        }

        public IActionResult AddArtWork()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddArtWork(ArtWorkInputViewModel artWorkInputViewModel)
        {
            this.ArtisticWorkService.AddArtWorkAsync(artWorkInputViewModel);

            return RedirectToAction("Categories", "ArtisticWork");
        }

        public IActionResult DeleteArtWork()
        {
            return View();
        }

        public IActionResult EditArtWork()
        {
            return View();
        }
    }
}