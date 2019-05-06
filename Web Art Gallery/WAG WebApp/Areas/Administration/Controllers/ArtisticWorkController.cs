using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

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
            var addArtWorkViewModel = new AddArtWorkViewModel()
            {
                ExistingCategories = this.ArtisticWorkService.GetArtisticWorkCategories()
            };

            return View(addArtWorkViewModel);
        }

        [HttpPost]
        public IActionResult AddArtWork(AddArtWorkViewModel addArtWorkViewModel)
        {
            this.ArtisticWorkService.AddArtWork(addArtWorkViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArtWork(int id)
        {
            var editArtWorkViewModel = ArtisticWorkService.GetEditArtWorkViewModel(id);

            return View(editArtWorkViewModel);
        }

        [HttpPost]
        public IActionResult EditArtWork(int id, EditArtWorkViewModel editArtWorkViewModel)
        {
            this.ArtisticWorkService.EditArtWork(id, editArtWorkViewModel);

            return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
        }

        public IActionResult DeleteArtWork(int id, ArtWorkDetailsViewModel artWorkDetailsViewModel)
        {
            artWorkDetailsViewModel.ArtisticWork = ArtisticWorkService.GetArtisticWorkById(id);

            return View(artWorkDetailsViewModel);
        }

        [HttpPost]
        public IActionResult DeleteArtWork(int id)
        {
            this.ArtisticWorkService.DeleteArtWork(id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            this.ArtisticWorkService.AddCategory(addCategoryViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult AllCategories(ArtWorkCategoriesViewModel artWorkCategoriesViewModel)
        {
            artWorkCategoriesViewModel.Categories = this.ArtisticWorkService.GetArtisticWorkCategories();

            return this.View(artWorkCategoriesViewModel);
        }

        public IActionResult EditCategory(int id, EditCategoryViewModel editCategoryViewModel)
        {
            var currCategory = this.ArtisticWorkService.GetCategoryById(id);

            editCategoryViewModel.CategoryId = id;

            editCategoryViewModel.CategoryName = currCategory.Name;

            editCategoryViewModel.PictureOld = currCategory.MainPicture;

            return this.View(editCategoryViewModel);
        }

        [HttpPost]
        public IActionResult EditCategory(int id, EditCategoryInputViewModel editCategoryInputViewModel)
        {
            this.ArtisticWorkService.EditCategory(id, editCategoryInputViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult DeleteCategory(int id, DeleteCategoryViewModel deleteCategoryViewModel)
        {
            deleteCategoryViewModel.ArtWorkCategory = this.ArtisticWorkService.GetCategoryById(id);

            return this.View(deleteCategoryViewModel);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            this.ArtisticWorkService.DeleteCategory(id);

            return RedirectToAction("Success", "Home", new { area = "" });
        }
    }
}