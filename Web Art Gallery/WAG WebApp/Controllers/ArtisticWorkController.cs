using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;
using WAG.WebApp.Controllers.Common;

namespace WAG.WebApp.Controllers
{
    public class ArtisticWorkController : BaseController
    {
        private readonly IArtisticWorkService ArtisticWorkService;
        private readonly ICloudinaryService cloudinaryService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService, ICloudinaryService cloudinaryService, IMapper mapper)
            : base(mapper)
        {
            this.ArtisticWorkService = artisticWorkService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Categories()
        {
            var existingArtWorkCategories = ArtisticWorkService.GetArtisticWorkCategories();

            var viewModel = new ArtWorkCategoriesViewModel();
            viewModel.Categories = mapper.Map<List<ArtWorkCategoryViewModel>>(existingArtWorkCategories);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(viewModel);
        }

        public IActionResult ArtWorksByCategory(int id, string availability, string price)
        {
            var filteredArtWorks = ArtisticWorkService.GetFilteredArtWorksByCategoryId(id, availability, price);
            var currentArtWorkCategory = this.ArtisticWorkService.GetCategoryById(id);

            if (currentArtWorkCategory == null || filteredArtWorks == null)
            {
                return RedirectToAction("Categories", "ArtisticWork");
            }

            var viewModel = new ArtWorkCollectionViewModel()
            {
                ArtWorkCollection = mapper.Map<IEnumerable<ArtWorkByCategoryViewModel>>(filteredArtWorks),
                ArtWorkCategory = mapper.Map<ArtWorkCategoryViewModel>(currentArtWorkCategory),
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            return View(viewModel);
        }

        public IActionResult ArtWorkDetails(int id)
        {

            var currArtWork = ArtisticWorkService.GetArtisticWorkById(id);
            if (currArtWork == null)
            {
                return RedirectToAction("Categories", "ArtisticWork");
            }
            
            var currArtWorkCategory = ArtisticWorkService.GetCategoryById(currArtWork.ArtisticWorkCategoryId);

            var viewModel = mapper.Map<ArtWorkDetailsViewModel>(currArtWork);
            viewModel.ArtWorkCategory = mapper.Map<ArtWorkCategoryViewModel>(currArtWorkCategory);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(viewModel);
        }
    }
}