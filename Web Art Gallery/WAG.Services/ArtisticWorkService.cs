﻿using System;
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
        private const string AvailableFilter = "available";
        private const string SoldFilter = "sold";
        private const string AscendingFilter = "ascending";
        private const string DescendingFilter = "descending";

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
                PictureFileName = addArtWorkViewModel.PictureFileName,
                CreatedOn = DateTime.UtcNow
            };

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
                if (categoryNew != null)
                {
                    artworkToUpdate.ArtisticWorkCategory = categoryNew;
                }
                artworkToUpdate.ArtisticWorkCategory = categoryNew;
                artworkToUpdate.Technique = editArtWorkViewModel.Technique;
                artworkToUpdate.EditedOn = DateTime.UtcNow;

                DbContext.ArtisticWorks.Update(artworkToUpdate);
                DbContext.SaveChanges();
            }
        }

        public void DeleteArtWork(int id)
        {
            var artWork = this.DbContext.ArtisticWorks.FirstOrDefault(p => p.Id == id);

            if (artWork != null)
            {
                this.DbContext.ArtisticWorks.Remove(artWork);
                this.DbContext.SaveChanges();
            }
        }

        public EditArtWorkViewModel GetEditArtWorkViewModel(int id)
        {
            var artWork = GetArtisticWorkById(id);

            if (artWork == null)
            {
                return null;
            }

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

        public List<ArtisticWork> GetArtWorksByCategoryIdAndFilter(int id, string availability, string price)
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

        public void AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            if (!DbContext.ArtisticWorkCategories.Any(x => x.Name == addCategoryViewModel.CategoryName))
            {
                var category = new ArtisticWorkCategory();

                category.Name = addCategoryViewModel.CategoryName;
                category.MainPictureFileName = addCategoryViewModel.PictureFileName;

                this.DbContext.ArtisticWorkCategories.Add(category);
                this.DbContext.SaveChanges();
            }
        }

        public void EditCategory(EditCategoryViewModel editCategoryViewModel)
        {
            ArtisticWorkCategory categoryToEdit = DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == editCategoryViewModel.CategoryId);
            if (categoryToEdit != null)
            {
                categoryToEdit.MainPictureFileName = editCategoryViewModel.PictureFileName;
                DbContext.ArtisticWorkCategories.Update(categoryToEdit);
                DbContext.SaveChanges();
            }
        }


        public void DeleteCategory(int categoryId)
        {
            var categoryToDelete = this.DbContext.ArtisticWorkCategories.FirstOrDefault(c => c.Id == categoryId);

            if (categoryToDelete != null)
            {
                this.DbContext.ArtisticWorkCategories.Remove(categoryToDelete);
                this.DbContext.SaveChanges();
            }
        }
    }
}
