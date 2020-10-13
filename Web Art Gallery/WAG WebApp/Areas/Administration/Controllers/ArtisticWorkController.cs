using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class ArtisticWorkController : AdministrationController
    {
        private IArtisticWorkService ArtisticWorkService;
        private ICloudinaryService cloudinaryService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService, ICloudinaryService cloudinaryService, IMapper mapper)
            : base(mapper)
        {
            this.ArtisticWorkService = artisticWorkService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult ManageArtWorks()
        {
            return View();
        }

        public IActionResult AddArtWork()
        {
            var viewModel = new AddArtWorkViewModel();

            var existingArtWorkCategories = this.ArtisticWorkService.GetArtisticWorkCategories();
            viewModel.ExistingCategories = mapper.Map<IEnumerable<ArtWorkCategoryViewModel>>(existingArtWorkCategories);

            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddArtWork(AddArtWorkViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var existingArtWorkCategories = this.ArtisticWorkService.GetArtisticWorkCategories();
                viewModel.ExistingCategories = mapper.Map<IEnumerable<ArtWorkCategoryViewModel>>(existingArtWorkCategories);

                return this.View(viewModel);
            }

            var newArtWork = mapper.Map<ArtisticWork>(viewModel);
            this.ArtisticWorkService.AddArtWork(newArtWork);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArtWork(int id)
        {
            ArtisticWork artWorkToEdit = this.ArtisticWorkService.GetArtisticWorkById(id);
            if (artWorkToEdit == null)
            {
               return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            var viewModel = mapper.Map<EditArtWorkViewModel>(artWorkToEdit);

            var existingArtWorkCategories = this.ArtisticWorkService.GetArtisticWorkCategories();
            viewModel.ExistingCategories = mapper.Map<IEnumerable<ArtWorkCategoryViewModel>>(existingArtWorkCategories);

            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditArtWork(EditArtWorkViewModel viewModel)
        {
            if (this.ArtisticWorkService.GetArtisticWorkById(viewModel.Id) == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            };

            ArtisticWork ArtWorkUpdate = mapper.Map<ArtisticWork>(viewModel);
            this.ArtisticWorkService.EditArtWork(ArtWorkUpdate);

            return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
        }

        public IActionResult DeleteArtWork(int id)
        {
            ArtisticWork artWorkToDelete = ArtisticWorkService.GetArtisticWorkById(id);

            if (artWorkToDelete == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            var ViewModel = mapper.Map<DeleteArtWorkViewModel>(artWorkToDelete);
            ViewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult DeleteArtWorkPost(int id)
        {
            if (this.ArtisticWorkService.GetArtisticWorkById(id) == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            };

            this.ArtisticWorkService.DeleteArtWork(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult AddCategory()
        {
            var viewModel = new AddCategoryViewModel() 
            { 
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance() 
            };
        
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(ViewModel);
            }

            var newArtWorkCategory = mapper.Map<ArtisticWorkCategory>(ViewModel);
            this.ArtisticWorkService.AddCategory(newArtWorkCategory);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult AllCategories(ArtWorkCategoriesViewModel viewModel)
        {
            var existingArtWorkCategories = this.ArtisticWorkService.GetArtisticWorkCategories();
            viewModel.Categories = mapper.Map<List<ArtWorkCategoryViewModel>>(existingArtWorkCategories);

            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }

        public IActionResult EditCategory(int id)
        {
            var categoryToEdit = this.ArtisticWorkService.GetCategoryById(id);

            if (categoryToEdit == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            var viewModel = mapper.Map<EditCategoryViewModel>(categoryToEdit);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel viewModel)
        {
            var currCategory = this.ArtisticWorkService.GetCategoryById(viewModel.CategoryId);

            if (currCategory == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            var ArtWorkCategoryUpdate = mapper.Map<ArtisticWorkCategory>(viewModel);

            this.ArtisticWorkService.EditCategory(ArtWorkCategoryUpdate);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var currCategory = this.ArtisticWorkService.GetCategoryById(id);

            if (currCategory == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            var viewModel = mapper.Map<DeleteCategoryViewModel>(currCategory);
            viewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteCategoryPost(int id)
        {
            if (this.ArtisticWorkService.GetCategoryById(id) == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            };

            this.ArtisticWorkService.DeleteCategory(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }
    }
}