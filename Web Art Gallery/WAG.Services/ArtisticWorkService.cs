using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.Services
{
    public class ArtisticWorkService : IArtisticWorkService
    {
        private WAGDbContext DbContext;
        private ICommonService CommonService;

        public ArtisticWorkService(WAGDbContext dbContext, ICommonService commonService)
        {
            this.DbContext = dbContext;
            this.CommonService = commonService;
        }

        public void AddArtWork(AddArtWorkViewModel addArtWorkViewModel)
        {
            var technique = addArtWorkViewModel.Technique;

            var category = this.DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == addArtWorkViewModel.CategoryId);
            
            var order = new Order()
            {
                OrderInfo = "Test"
            };

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
                Picture = this.CommonService.UploadPictureAsync(addArtWorkViewModel.Picture).Result,
                CreatedOn = DateTime.UtcNow
            };

            this.DbContext.ArtisticWorks.Add(artWork);

            this.DbContext.SaveChanges();
        }

        public void EditArtWork(int id, EditArtWorkViewModel editArtWorkViewModel)
        {
            var categoryNew = DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == editArtWorkViewModel.CategoryId);

            var currArtwork = this.DbContext.ArtisticWorks.FirstOrDefault(artwork => artwork.Id == id);

            if (currArtwork != null)
            {
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).Year = editArtWorkViewModel.Year;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).Height = editArtWorkViewModel.Height;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).Width = editArtWorkViewModel.Width;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).Price = editArtWorkViewModel.Price;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).Availability = editArtWorkViewModel.Availability;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).HasFrame = editArtWorkViewModel.HasFrame;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).ArtisticWorkCategory = categoryNew;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).Technique = editArtWorkViewModel.Technique;
                this.DbContext.ArtisticWorks.First(artwork => artwork.Id == id).EditedOn = DateTime.UtcNow;
                DbContext.SaveChanges();
            }
        }

        public void DeleteArtWork(int id)
        {
            var product = this.DbContext.ArtisticWorks.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                this.DbContext.ArtisticWorks.Remove(product);
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

                category.MainPicture = this.CommonService.UploadPictureAsync(addCategoryViewModel.Picture).Result;

                this.DbContext.ArtisticWorkCategories.Add(category);

                this.DbContext.SaveChanges();
            }
        }

        public void EditCategory(int CategoryId, EditCategoryInputViewModel editCategoryInputViewModel)
        {
            var PictureNew = this.CommonService.UploadPictureAsync(editCategoryInputViewModel.PictureNew).Result;

            if (this.DbContext.ArtisticWorkCategories.Any(c => c.Id == CategoryId))
            {
                DbContext.ArtisticWorkCategories.First(c => c.Id == CategoryId).MainPicture = PictureNew;

                DbContext.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var currCategory = this.DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == categoryId);

            if (currCategory != null)
            {
                this.DbContext.ArtisticWorkCategories.Remove(currCategory);

                this.DbContext.SaveChanges();
            }
        }
    }
}
