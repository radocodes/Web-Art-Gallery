using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Constants;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.Services
{
    public class ArtisticWorkService : IArtisticWorkService
    {
        private WAGDbContext DbContext;
        private IFileService FileService;

        public ArtisticWorkService(WAGDbContext dbContext, IFileService fileService)
        {
            this.DbContext = dbContext;
            this.FileService = fileService;
        }

        public void AddArtWork(AddArtWorkViewModel addArtWorkViewModel)
        {
            var technique = addArtWorkViewModel.Technique;

            var category = this.DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == addArtWorkViewModel.CategoryId);

            var artWork = new ArtisticWork()
            {
                Year = addArtWorkViewModel.Year,
                Height = addArtWorkViewModel.Height,
                Width = addArtWorkViewModel.Width,
                Price = addArtWorkViewModel.Price,
                Availability = addArtWorkViewModel.Availability,
                HasFrame = addArtWorkViewModel.HasFrame,
                ArtisticWorkCategory = category,
                Technique = technique,
                CreatedOn = DateTime.UtcNow
            };

            string imgFileName = $"{Guid.NewGuid()}{GlobalConstants.JpegFileExtension}";

            if (addArtWorkViewModel.Picture != null)
            {
                artWork.PictureFileName = this.FileService.UploadImageAsync(Constants.GlobalConstants.ArtWorksImageDirectoryPath, imgFileName, addArtWorkViewModel.Picture).Result;
            }

            this.DbContext.ArtisticWorks.Add(artWork);
            this.DbContext.SaveChanges();
        }

        public void EditArtWork(int id, EditArtWorkViewModel editArtWorkViewModel)
        {
            var categoryNew = DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == editArtWorkViewModel.CategoryId);

            var artworkToUpdate = this.DbContext.ArtisticWorks.FirstOrDefault(artwork => artwork.Id == id);

            if (artworkToUpdate != null)
            {
                artworkToUpdate.Year = editArtWorkViewModel.Year;
                artworkToUpdate.Height = editArtWorkViewModel.Height;
                artworkToUpdate.Width = editArtWorkViewModel.Width;
                artworkToUpdate.Price = editArtWorkViewModel.Price;
                artworkToUpdate.Availability = editArtWorkViewModel.Availability;
                artworkToUpdate.HasFrame = editArtWorkViewModel.HasFrame;
                artworkToUpdate.ArtisticWorkCategory = categoryNew;
                artworkToUpdate.Technique = editArtWorkViewModel.Technique;
                artworkToUpdate.EditedOn = DateTime.UtcNow;

                DbContext.SaveChanges();
            }
        }

        public void DeleteArtWork(int id)
        {
            var artWork = this.DbContext.ArtisticWorks.FirstOrDefault(p => p.Id == id);

            if (artWork != null)
            {
                var artWorkImgFileName = artWork.PictureFileName;

                if (File.Exists($"{GlobalConstants.ArtWorksImageDirectoryPath}{artWorkImgFileName}"))
                {
                    File.Delete($"{GlobalConstants.ArtWorksImageDirectoryPath}{artWorkImgFileName}");
                }

                this.DbContext.ArtisticWorks.Remove(artWork);
                this.DbContext.SaveChanges();
            }
        }

        public EditArtWorkViewModel GetEditArtWorkViewModel(int id)
        {
            var artWork = GetArtisticWorkById(id);

            var viewModel = new EditArtWorkViewModel()
            {
                Year = artWork.Year,
                Height = artWork.Height,
                Width = artWork.Width,
                Price = artWork.Price,
                Availability = artWork.Availability,
                HasFrame = artWork.HasFrame,
                Technique = artWork.Technique,
                ExistingCategories = this.GetArtisticWorkCategories()
            };

            return viewModel;
        }

        public List<ArtisticWorkCategory> GetArtisticWorkCategories()
        {
            var categories = this.DbContext.ArtisticWorkCategories.ToList();

            return categories;
        }

        public List<ArtisticWork> GetArtWorksByCategoryId(int id)
        {
            var artworks = DbContext.ArtisticWorks.Where(x => x.ArtisticWorkCategoryId == id).OrderByDescending(x => x.Id).ToList();

            return artworks;
        }

        public ArtisticWork GetArtisticWorkById(int id)
        {
            var artWork = DbContext.ArtisticWorks.FirstOrDefault(x => x.Id == id);

            return artWork;
        }

        public ArtisticWorkCategory GetCategoryById(int id)
        {
            var category = DbContext.ArtisticWorkCategories.FirstOrDefault(x => x.Id == id);

            return category;
        }

        public void AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            if (!DbContext.ArtisticWorkCategories.Any(x => x.Name == addCategoryViewModel.CategoryName))
            {
                var category = new ArtisticWorkCategory();

                category.Name = addCategoryViewModel.CategoryName;

                if (addCategoryViewModel.Picture != null)
                {
                    var imgFileName = $"{Guid.NewGuid()}{GlobalConstants.JpegFileExtension}";

                    category.MainPictureFileName = this.FileService.UploadImageAsync(Constants.GlobalConstants.ArtCategoriesDirectoryPath, imgFileName, addCategoryViewModel.Picture).Result;
                }

                this.DbContext.ArtisticWorkCategories.Add(category);
                this.DbContext.SaveChanges();
            }
        }

        public void EditCategory(int CategoryId, EditCategoryInputViewModel editCategoryInputViewModel)
        {
            if (this.DbContext.ArtisticWorkCategories.Any(c => c.Id == CategoryId) && editCategoryInputViewModel.PictureNew != null)
            {
                var imgFileName = $"{Guid.NewGuid()}{GlobalConstants.JpegFileExtension}";

                if (editCategoryInputViewModel.PictureNew != null)
                {
                    var oldImageFileName = DbContext.ArtisticWorkCategories.First(c => c.Id == CategoryId).MainPictureFileName;

                    if (File.Exists($"{GlobalConstants.ArtCategoriesDirectoryPath}{oldImageFileName}"))
                    {
                        File.Delete($"{GlobalConstants.ArtCategoriesDirectoryPath}{oldImageFileName}");
                    }

                    var newImageFileName = this.FileService.UploadImageAsync(GlobalConstants.ArtCategoriesDirectoryPath, imgFileName, editCategoryInputViewModel.PictureNew).Result;

                    DbContext.ArtisticWorkCategories.First(c => c.Id == CategoryId).MainPictureFileName = newImageFileName;
                    DbContext.SaveChanges();
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var currCategory = this.DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == categoryId);

            if (currCategory != null)
            {
                var categoryImgFileName = currCategory.MainPictureFileName;

                if (File.Exists($"{GlobalConstants.ArtCategoriesDirectoryPath}{categoryImgFileName}"))
                {
                    File.Delete($"{GlobalConstants.ArtCategoriesDirectoryPath}{categoryImgFileName}");
                }

                this.DbContext.ArtisticWorkCategories.Remove(currCategory);
                this.DbContext.SaveChanges();
            }
        }
    }
}
