using Microsoft.EntityFrameworkCore;
using System.Linq;
using WAG.Data;
using WAG.Data.Models;
using WAG.ViewModels.ArtisticWorks;
using Xunit;

namespace WAG.Services.Tests
{
    public class ArtisticWorkServiceTests
    {
        [Fact]
        public void AddArtWorkShouldAddsArtworkCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_ArtWork_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext, null);

            var addArtWorkViewModel = new AddArtWorkViewModel()
            {
                Year = 2015,
                Height = 120.5,
                Width = 80.2,
                Price = 350.99m,
                Availability = true,
                HasFrame = true,
                Technique = "Test Technique",
            };

            service.AddArtWork(addArtWorkViewModel);

            var artWork = dbContext.ArtisticWorks.FirstOrDefault();

            Assert.NotNull(artWork);
            Assert.Equal(addArtWorkViewModel.Year, artWork.Year);
            Assert.Equal(addArtWorkViewModel.Height, artWork.Height);
            Assert.Equal(addArtWorkViewModel.Width, artWork.Width);
            Assert.Equal(addArtWorkViewModel.Price, artWork.Price);
            Assert.Equal(addArtWorkViewModel.Availability, artWork.Availability);
            Assert.Equal(addArtWorkViewModel.HasFrame, artWork.HasFrame);
            Assert.Equal(addArtWorkViewModel.Technique, artWork.Technique);
        }

        [Fact]
        public void EditArtWorkShouldEditsArtworkCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_ArtWork_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext, null);

            var artisticWork = new ArtisticWork()
            {
                Year = 2015,
                Height = 120.5,
                Width = 80.2,
                Price = 350.99m,
                Availability = true,
                HasFrame = true,
                Technique = "Test Technique",
            };

            dbContext.ArtisticWorks.Add(artisticWork);
            dbContext.SaveChanges();

            var editArtWorkViewModel = new EditArtWorkViewModel()
            {
                Year = 2016,
                Height = 130.6,
                Width = 70.3,
                Price = 420.50m,
                Availability = false,
                HasFrame = false,
                Technique = "Test Technique - Edited",
            };

            var id = dbContext.ArtisticWorks.LastOrDefault().Id;

            service.EditArtWork(id, editArtWorkViewModel);

            var artWork = dbContext
                .ArtisticWorks
                .FirstOrDefault(artwork => artwork.Id == id);

            Assert.Equal(editArtWorkViewModel.Year, artWork.Year);
            Assert.Equal(editArtWorkViewModel.Height, artWork.Height);
            Assert.Equal(editArtWorkViewModel.Width, artWork.Width);
            Assert.Equal(editArtWorkViewModel.Price, artWork.Price);
            Assert.Equal(editArtWorkViewModel.Availability, artWork.Availability);
            Assert.Equal(editArtWorkViewModel.HasFrame, artWork.HasFrame);
            Assert.Equal(editArtWorkViewModel.Technique, artWork.Technique);
        }

        [Fact]
        public void DeleteArtWorkShouldDeleteArtworkCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Delete_ArtWork_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext, null);

            var artisticWork = new ArtisticWork();

            dbContext.ArtisticWorks.Add(artisticWork);
            dbContext.SaveChanges();

            var artWorkId = dbContext.ArtisticWorks.LastOrDefault().Id;

            service.DeleteArtWork(artWorkId);

            Assert.True(dbContext
                .ArtisticWorks
                .Any(artwork => artwork.Id == artWorkId) == false);
        }

        [Fact]
        public void GetArtisticWorkCategoriesShouldReturnsCategories()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_ArtWork_Category_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            var categoriesCount = 3;

            for (int i = 0; i < categoriesCount; i++)
            {
                dbContext.ArtisticWorkCategories.Add(new ArtisticWorkCategory());
            }

            dbContext.SaveChanges();

            var service = new ArtisticWorkService(dbContext, null);

            var artWorkCategories = service.GetArtisticWorkCategories();

            Assert.Equal(categoriesCount, artWorkCategories.Count);
        }

        [Fact]
        public void GetArtWorksByCategoryIdShouldReturnsArtWorks()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_ArtWork_By_CategoryId_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            //added few artworks with same category
            var sameCategoryArtworksCount = 2;
            var FirstMockCategoryId = 1;

            for (int i = 0; i < sameCategoryArtworksCount; i++)
            {
                dbContext.ArtisticWorks
                    .Add(new ArtisticWork()
                    {
                        ArtisticWorkCategoryId = FirstMockCategoryId,
                    });
            }

            //add artwork with different category
            var SecondMockCategoryId = 5;
            dbContext.ArtisticWorks
                .Add(new ArtisticWork()
                {
                    ArtisticWorkCategoryId = SecondMockCategoryId,
                });

            dbContext.SaveChanges();

            var service = new ArtisticWorkService(dbContext, null);

            var artWorks = service.GetArtWorksByCategoryId(FirstMockCategoryId);

            Assert.Equal(sameCategoryArtworksCount, artWorks.Count);

        }

        [Fact]
        public void GetArtisticWorkByIdShouldReturnsArtWork()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_ArtWork_By_Id_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            //seed some random artworks
            for (int i = 0; i < 3; i++)
            {
                dbContext.ArtisticWorks.Add(new ArtisticWork());
            }

            //seed special artwork with unique year and save its id
            var artworkYear = 2010;
            dbContext.ArtisticWorks.Add(new ArtisticWork()
            {
                Year = artworkYear
            });

            dbContext.SaveChanges();

            var artWorkId = dbContext.ArtisticWorks.LastOrDefault().Id;

            //seed some random artworks again 
            for (int i = 0; i < 3; i++)
            {
                dbContext.ArtisticWorks.Add(new ArtisticWork());
            }

            var service = new ArtisticWorkService(dbContext, null);

            var artWork = service.GetArtisticWorkById(artWorkId);

            Assert.Equal(artWorkId, artWork.Id);
            Assert.Equal(artworkYear, artWork.Year);
        }

        [Fact]
        public void GetCategoryByIdShouldReturnsCategory()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_Artwork_Category_By_Id_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            //seed some random Categories
            for (int i = 0; i < 3; i++)
            {
                dbContext.ArtisticWorkCategories.Add(new ArtisticWorkCategory());
            }

            //seed special Category with unique name and save its id
            var categoryName = "Mock Category Name";
            dbContext.ArtisticWorkCategories.Add(new ArtisticWorkCategory()
            {
                Name = categoryName
            });

            dbContext.SaveChanges();

            var categoryId = dbContext.ArtisticWorkCategories.LastOrDefault().Id;

            //seed some random categories again 
            for (int i = 0; i < 3; i++)
            {
                dbContext.ArtisticWorkCategories.Add(new ArtisticWorkCategory());
            }

            var service = new ArtisticWorkService(dbContext, null);

            var category = service.GetCategoryById(categoryId);

            Assert.Equal(categoryId, category.Id);
            Assert.Equal(categoryName, category.Name);
        }

        [Fact]
        public void AddCategoryShouldAddCategoryCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_Category_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext, null);

            var categoryName = "Add Category - test";

            var addCategoryViewModel = new AddCategoryViewModel()
            {
                CategoryName = categoryName
            };

            service.AddCategory(addCategoryViewModel);

            Assert.True(dbContext
                .ArtisticWorkCategories
                .Any(cat => cat.Name == categoryName));
        }

        [Fact]
        public void DeleteCategoryShouldDeleteCategoryCorrect()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Delete_Category_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext, null);

            var categoryName = "Test Category Name";

            var categoryToEdit = new ArtisticWorkCategory()
            {
                Name = "Test Category Name",
            };

            dbContext.ArtisticWorkCategories.Add(categoryToEdit);
            dbContext.SaveChanges();

            var categoryId = dbContext.ArtisticWorkCategories.LastOrDefault().Id;

            var storedCategory = dbContext
                .ArtisticWorkCategories
                .FirstOrDefault(category => category.Id == categoryId);

            Assert.Equal(categoryName, storedCategory.Name);

            service.DeleteCategory(categoryId);

            Assert.False(dbContext
                .ArtisticWorkCategories
                .Any(cat => cat.Id == categoryId));
        }
    }
}
