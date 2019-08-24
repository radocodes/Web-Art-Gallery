using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.WebApp.Areas.BG.Controllers
{
    public class ArtisticWorkController : BGController
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
            var viewModel = new ArtWorkDetailsViewModel();

            viewModel.ArtisticWork = ArtisticWorkService.GetArtisticWorkById(id);
            viewModel.ArtisticWork.ArtisticWorkCategory = ArtisticWorkService.GetCategoryById(viewModel.ArtisticWork.ArtisticWorkCategoryId);

            return View(viewModel);
        }
    }
}