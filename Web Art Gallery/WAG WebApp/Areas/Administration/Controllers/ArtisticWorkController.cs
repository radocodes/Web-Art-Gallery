using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels;
using WAG.ViewModels.InputViewModels;
using WAG.ViewModels.OutputViewModels;

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

        public IActionResult EditArtWork(int id)
        {
            var editArtWorkViewModel = ArtisticWorkService.GetEditArtWorkViewModel(id);

            return View(editArtWorkViewModel);
        }

        [HttpPost]
        public IActionResult EditArtWork(int id, ArtWorkInputViewModel artWorkInputViewModel)
        {
            this.ArtisticWorkService.EditArtWork(id, artWorkInputViewModel);

            return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
        }

        public IActionResult DeleteArtWork(int id, ArtWorkViewModel artWorkViewModel)
        {
            artWorkViewModel.ArtisticWork = ArtisticWorkService.GetArtisticWorkById(id);

            return View(artWorkViewModel);
        }

        [HttpPost]
        public IActionResult DeleteArtWork(int id)
        {
            this.ArtisticWorkService.DeleteArtWork(id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        
    }
}