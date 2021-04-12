using System;
using System.Collections.Generic;
using System.Linq;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class ArtisticWorkService : IArtisticWorkService
    {
        private const string AvailableFilter = "available";
        private const string SoldFilter = "sold";
        private const string AscendingFilter = "ascending";
        private const string DescendingFilter = "descending";

        private readonly WAGDbContext DbContext;
        private readonly IFileService FileService;

        public ArtisticWorkService(WAGDbContext dbContext, IFileService fileService)
        {
            this.DbContext = dbContext;
            this.FileService = fileService;
        }

        public void AddArtWork(ArtisticWork newArtWork)
        {
            newArtWork.ArtisticWorkCategory = this.DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == newArtWork.ArtisticWorkCategoryId);
            newArtWork.CreatedOn = DateTime.UtcNow;

            this.DbContext.ArtisticWorks.Add(newArtWork);
            this.DbContext.SaveChanges();
        }

        public void EditArtWork(ArtisticWork artWorkUpdate)
        {
            var existingCurrentArtwork = this.GetArtisticWorkById(artWorkUpdate.Id);

            if (existingCurrentArtwork == null)
            {
                return;
            }

            var categoryNew = DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == artWorkUpdate.ArtisticWorkCategoryId);
            if (categoryNew != null)
            {
                artWorkUpdate.ArtisticWorkCategory = categoryNew;
            }

            artWorkUpdate.EditedOn = DateTime.UtcNow;

            DbContext.ArtisticWorks.Update(artWorkUpdate);
            DbContext.SaveChanges();
        }

        public void DeleteArtWork(int id)
        {
            var artWork = this.GetArtisticWorkById(id);

            if (artWork != null)
            {
                this.DbContext.ArtisticWorks.Remove(artWork);
                this.DbContext.SaveChanges();
            }
        }

        public IEnumerable<ArtisticWorkCategory> GetArtisticWorkCategories()
        {
            var categories = this.DbContext.ArtisticWorkCategories.ToList();

            return categories;
        }

        public IEnumerable<ArtisticWork> GetArtWorksByCategoryId(int id)
        {
            var artworks = DbContext.ArtisticWorks.Where(x => x.ArtisticWorkCategoryId == id).OrderByDescending(x => x.Id).ToList();

            return artworks;
        }

        public IEnumerable<ArtisticWork> GetFilteredArtWorksByCategoryId(int id, string availability, string price)
        {
            List<ArtisticWork> artworks;

            if (availability == AvailableFilter)
            {

                artworks = DbContext
                .ArtisticWorks
                .Where(artwork => artwork.ArtisticWorkCategoryId == id && artwork.Availability == true).ToList();
            }

            else if (availability == SoldFilter)
            {

                artworks = DbContext
                .ArtisticWorks
                .Where(artwork => artwork.ArtisticWorkCategoryId == id && artwork.Availability == false).ToList();
            }

            else
            {
                artworks = DbContext
                    .ArtisticWorks
                    .Where(artwork => artwork.ArtisticWorkCategoryId == id)
                    .ToList();
            }

            if (price == AscendingFilter)
            {
                artworks = artworks.OrderBy(artwork => artwork.Price).ThenByDescending(order => order.Id).ToList();
            }

            else if (price == DescendingFilter)
            {
                artworks = artworks.OrderByDescending(artwork => artwork.Price).ThenByDescending(order => order.Id).ToList();
            }

            else
            {
                artworks.OrderByDescending(order => order.Id);
            }

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

        public void AddCategory(ArtisticWorkCategory newArtWorkCategory)
        {
            if (!DbContext.ArtisticWorkCategories.Any(x => x.Name == newArtWorkCategory.Name))
            {
                this.DbContext.ArtisticWorkCategories.Add(newArtWorkCategory);
                this.DbContext.SaveChanges();
            }
        }

        public void EditCategory(ArtisticWorkCategory artWorkCategoryUpdate)
        {
            ArtisticWorkCategory ExistingCurrentCategory = this.GetCategoryById(artWorkCategoryUpdate.Id);
            if (ExistingCurrentCategory != null)
            {
                ExistingCurrentCategory.MainPictureFileName = artWorkCategoryUpdate.MainPictureFileName;
                DbContext.ArtisticWorkCategories.Update(ExistingCurrentCategory);

                DbContext.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var categoryToDelete = this.GetCategoryById(categoryId);

            if (categoryToDelete != null)
            {
                this.DbContext.ArtisticWorkCategories.Remove(categoryToDelete);
                this.DbContext.SaveChanges();
            }
        }
    }
}
