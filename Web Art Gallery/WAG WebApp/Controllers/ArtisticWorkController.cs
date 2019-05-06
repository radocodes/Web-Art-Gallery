using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;


namespace WAG.WebApp.Controllers
{
    public class ArtisticWorkController : Controller
    {
        private IArtisticWorkService ArtisticWorkService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService)
        {
            this.ArtisticWorkService = artisticWorkService;
        }

        public IActionResult Categories(ArtWorkCategoriesViewModel categoriesViewModel)
        {
            categoriesViewModel.Categories = ArtisticWorkService.GetArtisticWorkCategories();

            return View(categoriesViewModel);
        }

        public IActionResult ArtWorksByCategory(int id)
        {
            var artWorkViewModel = new ArtWorkCollectionViewModel()
            {
                ArtWorkCollection = ArtisticWorkService.GetArtWorksByCategoryId(id),
                ArtWorkCategory = ArtisticWorkService.GetCategoryById(id)
            };

            return View(artWorkViewModel);
        }

        public IActionResult CatalogAll()
        {
            return View();
        }

        public IActionResult ArtWorkDetails(int id)
        {
            var viewModel = new ArtWorkDetailsViewModel()
            {
                ArtisticWork = ArtisticWorkService.GetArtisticWorkById(id),
            };

            return View(viewModel);
        }
    }
}