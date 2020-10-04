using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class ArtisticWorkController : AdministrationController
    {
        private IArtisticWorkService ArtisticWorkService;
        private ICloudinaryService cloudinaryService;

        public ArtisticWorkController(IArtisticWorkService artisticWorkService, ICloudinaryService cloudinaryService)
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
            var addArtWorkViewModel = new AddArtWorkViewModel()
            {
                ExistingCategories = this.ArtisticWorkService.GetArtisticWorkCategories()
            };

            addArtWorkViewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

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

            editArtWorkViewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

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

            if (artWorkDetailsViewModel.ArtisticWork == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            artWorkDetailsViewModel.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

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
            var addCategoryViewModel = new AddCategoryViewModel() 
            { 
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance() 
            };
        
            return this.View(addCategoryViewModel);
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

        public IActionResult EditCategory(int id)
        {
            var categoryToEdit = this.ArtisticWorkService.GetCategoryById(id);

            if (categoryToEdit == null)
            {
                return RedirectToAction("Categories", "ArtisticWork", new { area = "" });
            }

            var viewModel = new EditCategoryViewModel()
            {
                CategoryId = categoryToEdit.Id,
                CategoryName = categoryToEdit.Name,
                PictureFileName = categoryToEdit.MainPictureFileName,

                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

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

            this.ArtisticWorkService.EditCategory(viewModel);

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

            var deleteCategoryViewModel = new DeleteCategoryViewModel()
            {
                ArtWorkCategory = this.ArtisticWorkService.GetCategoryById(id),
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            return this.View(deleteCategoryViewModel);
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