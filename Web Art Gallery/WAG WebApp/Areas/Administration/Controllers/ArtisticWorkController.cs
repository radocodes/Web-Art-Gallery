using Microsoft.AspNetCore.Mvc;
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
            if (!ModelState.IsValid)
            {
                addArtWorkViewModel.ExistingCategories = this.ArtisticWorkService.GetArtisticWorkCategories();

                return this.View(addArtWorkViewModel);
            }

            this.ArtisticWorkService.AddArtWork(addArtWorkViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult EditArtWork(int id)
        {
            var editArtWorkViewModel = ArtisticWorkService.GetEditArtWorkViewModel(id);

            if (editArtWorkViewModel == null)
            {
               return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            return View(editArtWorkViewModel);
        }

        [HttpPost]
        public IActionResult EditArtWork(int id, EditArtWorkViewModel editArtWorkViewModel)
        {
            if (this.ArtisticWorkService.GetArtisticWorkById(id) == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            };

            this.ArtisticWorkService.EditArtWork(id, editArtWorkViewModel);

            return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
        }

        public IActionResult DeleteArtWork(int id, ArtWorkDetailsViewModel artWorkDetailsViewModel)
        {
            artWorkDetailsViewModel.ArtisticWork = ArtisticWorkService.GetArtisticWorkById(id);

            if (artWorkDetailsViewModel == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            return View(artWorkDetailsViewModel);
        }

        [HttpPost]
        public IActionResult DeleteArtWork(int id)
        {
            if (this.ArtisticWorkService.GetArtisticWorkById(id) == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            };

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
            if (!ModelState.IsValid)
            {
                return this.View(addCategoryViewModel);
            }

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

            if (currCategory == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            editCategoryViewModel.CategoryId = id;

            editCategoryViewModel.CategoryName = currCategory.Name;

            editCategoryViewModel.PictureOld = currCategory.MainPictureFileName;

            return this.View(editCategoryViewModel);
        }

        [HttpPost]
        public IActionResult EditCategory(int id, EditCategoryInputViewModel editCategoryInputViewModel)
        {
            var currCategory = this.ArtisticWorkService.GetCategoryById(id);

            if (currCategory == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            this.ArtisticWorkService.EditCategory(id, editCategoryInputViewModel);

            return RedirectToAction("Success", "Home", new { area = "" });
        }

        public IActionResult DeleteCategory(int id, DeleteCategoryViewModel deleteCategoryViewModel)
        {
            var currCategory = this.ArtisticWorkService.GetCategoryById(id);

            if (currCategory == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            deleteCategoryViewModel.ArtWorkCategory = this.ArtisticWorkService.GetCategoryById(id);

            return this.View(deleteCategoryViewModel);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
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