using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.InputViewModels;

namespace WAG.WebApp.Controllers
{
    public class ArtisticWorkController : Controller
    {
        private IArtisticWorkService ArtisticWorkService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService)
        {
            this.ArtisticWorkService = artisticWorkService;
        }

        public IActionResult CatalogAll()
        {
            return View();
        }

        public IActionResult ArtWorkDetails()
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
            this.ArtisticWorkService.AddArtWork(artWorkInputViewModel);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteArtWork()
        {
            return View();
        }

        //public IActionResult EditArtWork()
        //{
        //    return View();
        //}
    }
}