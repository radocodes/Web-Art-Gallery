using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.WebApp.Controllers
{
    public class ArtisticWorkController : Controller
    {
        private IArtisticWorkService ArtisticWorkService;
        private ICloudinaryService cloudinaryService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService, ICloudinaryService cloudinaryService)
        {
            this.ArtisticWorkService = artisticWorkService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Categories(ArtWorkCategoriesViewModel categoriesViewModel)
        {
            categoriesViewModel.Categories = ArtisticWorkService.GetArtisticWorkCategories();
            categoriesViewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(categoriesViewModel);
        }

        public IActionResult ArtWorksByCategory(int id, string availability, string price)
        {
            var artWorkViewModel = new ArtWorkCollectionViewModel()
            {
                ArtWorkCollection = ArtisticWorkService.GetArtWorksByCategoryIdAndFilter(id, availability, price),
                ArtWorkCategory = this.ArtisticWorkService.GetCategoryById(id),
            };

            if (artWorkViewModel.ArtWorkCollection == null || artWorkViewModel.ArtWorkCategory == null)
            {
                return RedirectToAction("Categories", "ArtisticWork");
            }

            artWorkViewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(artWorkViewModel);
        }

        public IActionResult ArtWorkDetails(int id)
        {
            var viewModel = new ArtWorkDetailsViewModel();

            viewModel.ArtisticWork = ArtisticWorkService.GetArtisticWorkById(id);

            if (viewModel.ArtisticWork == null)
            {
                return RedirectToAction("Categories", "ArtisticWork");
            }

            viewModel.ArtisticWork.ArtisticWorkCategory = ArtisticWorkService.GetCategoryById(viewModel.ArtisticWork.ArtisticWorkCategoryId);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(viewModel);
        }
    }
}