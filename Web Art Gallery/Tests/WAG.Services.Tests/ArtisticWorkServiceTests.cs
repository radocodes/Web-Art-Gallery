using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Implementation;
using Xunit;

namespace WAG.Services.Tests
{
    public class ArtisticWorkServiceTests
    {
        [Fact]
        public void AddArtWorkShouldAddsArtworkCorrect()
        {
            // Arrange 

            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_ArtWork_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext);

            var ArtWorkToAdd = new ArtisticWork()
            {
                Year = 2015,
                Height = 120.5,
                Width = 80.2,
                Price = 350.99m,
                Availability = true,
                HasFrame = true,
                Technique = "Test Technique",
                ArtisticWorkCategoryId = 5,
                PictureFileName = "Test_Guid.23AnMd9*!sdpfok"
            };

            // Act
            service.AddArtWork(ArtWorkToAdd);

            var artWork = dbContext.ArtisticWorks.FirstOrDefault();

            // Assert
            Assert.NotNull(artWork);
            Assert.Equal(ArtWorkToAdd.Year, artWork.Year);
            Assert.Equal(ArtWorkToAdd.Height, artWork.Height);
            Assert.Equal(ArtWorkToAdd.Width, artWork.Width);
            Assert.Equal(ArtWorkToAdd.Price, artWork.Price);
            Assert.Equal(ArtWorkToAdd.Availability, artWork.Availability);
            Assert.Equal(ArtWorkToAdd.HasFrame, artWork.HasFrame);
            Assert.Equal(ArtWorkToAdd.Technique, artWork.Technique);
            Assert.Equal(ArtWorkToAdd.ArtisticWorkCategoryId, artWork.ArtisticWorkCategoryId);
            Assert.Equal(ArtWorkToAdd.PictureFileName, artWork.PictureFileName);
            Assert.True(artWork.CreatedOn > DateTime.MinValue);
        }

        [Fact]
        public void EditArtWorkShouldEditsArtworkCorrect()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_ArtWork_Db")
                .Options;

            var dbContext = new WAGDbContext(options);

            var service = new ArtisticWorkService(dbContext);

            var artWorkSeed = new ArtisticWork()
            {
                Year = 2015,
                Height = 120.5,
                Width = 80.2,
                Price = 350.99m,
                Availability = true,
                HasFrame = true,
                Technique = "Test Technique",
                ArtisticWorkCategoryId = 5,
                PictureFileName = "test_GUID_abc123",
                CreatedOn = DateTime.UtcNow
            };

            dbContext.ArtisticWorks.Add(artWorkSeed);
            dbContext.SaveChanges();

            var artworkToUpdate = dbContext.ArtisticWorks.FirstOrDefault();

            artworkToUpdate.Year = 2016;
            artworkToUpdate.Height = 130.6;
            artworkToUpdate.Width = 70.3;
            artworkToUpdate.Price = 420.50m;
            artworkToUpdate.Availability = false;
            artworkToUpdate.HasFrame = false;
            artworkToUpdate.Technique = "Test Technique - Edited";
            artworkToUpdate.ArtisticWorkCategoryId = 4;
            artworkToUpdate.PictureFileName = "test_GUID_321cba";

            // Act
            service.EditArtWork(artworkToUpdate);

            var updatedArtWork = dbContext
                .ArtisticWorks
                .FirstOrDefault(artwork => artwork.Id == artworkToUpdate.Id);

            // Assert
            Assert.Equal(artworkToUpdate.Year, updatedArtWork.Year);
            Assert.Equal(artworkToUpdate.Height, updatedArtWork.Height);
            Assert.Equal(artworkToUpdate.Width, updatedArtWork.Width);
            Assert.Equal(artworkToUpdate.Price, updatedArtWork.Price);
            Assert.Equal(artworkToUpdate.Availability, updatedArtWork.Availability);
            Assert.Equal(artworkToUpdate.HasFrame, updatedArtWork.HasFrame);
            Assert.Equal(artworkToUpdate.Technique, updatedArtWork.Technique);
            Assert.Equal(artworkToUpdate.ArtisticWorkCategoryId, updatedArtWork.ArtisticWorkCategoryId);
            Assert.Equal(artworkToUpdate.PictureFileName, updatedArtWork.PictureFileName);
            Assert.Equal(artWorkSeed.CreatedOn, updatedArtWork.CreatedOn);
            Assert.True(updatedArtWork.EditedOn > DateTime.MinValue);
        }

        [Fact]
        public void DeleteArtWorkShouldDeleteArtworkCorrect()
        {
            // Arrange 

            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Delete_ArtWork_Db")
               .Options;

            var dbContext = new WAGDbContext(options);
            var service = new ArtisticWorkService(dbContext);
            var artisticWork = new ArtisticWork();

            dbContext.ArtisticWorks.Add(artisticWork);
            dbContext.SaveChanges();

            var artWorkId = dbContext.ArtisticWorks.LastOrDefault().Id;

            // Act
            service.DeleteArtWork(artWorkId);

            //Assert
            Assert.True(dbContext
                .ArtisticWorks
                .Any(artwork => artwork.Id == artWorkId) == false);
        }

        [Fact]
        public void GetArtisticWorkCategoriesShouldReturnsCategories()
        {
            // Arrange 

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

            var service = new ArtisticWorkService(dbContext);

            // Act
            var artWorkCategories = service.GetArtisticWorkCategories();

            // Assert
            Assert.Equal(categoriesCount, artWorkCategories.Count());
        }

        [Fact]
        public void GetArtWorksByCategoryIdShouldReturnsArtWorks()
        {
            // Arrange 

            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_ArtWork_By_CategoryId_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            // Seed a few artworks with same category
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

            // Seed artwork with different category
            var SecondMockCategoryId = 5;
            dbContext.ArtisticWorks
                .Add(new ArtisticWork()
                {
                    ArtisticWorkCategoryId = SecondMockCategoryId,
                });

            dbContext.SaveChanges();

            var service = new ArtisticWorkService(dbContext);

            // Act
            var artWorks = service.GetArtWorksByCategoryId(FirstMockCategoryId);

            // Assert
            Assert.Equal(sameCategoryArtworksCount, artWorks.Count());

        }

        [Fact]
        public void GetArtisticWorkByIdShouldReturnsArtWork()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_ArtWork_By_Id_Db")
               .Options;

            var dbContext = new WAGDbContext(options);

            //seed some random artworks
            for (int i = 0; i < 3; i++)
            {
                dbContext.ArtisticWorks.Add(new ArtisticWork());
            }

            //seed special artwork with unique year and save it's id
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

            var service = new ArtisticWorkService(dbContext);

            // Act
            var artWork = service.GetArtisticWorkById(artWorkId);

            // Assert
            Assert.Equal(artWorkId, artWork.Id);
            Assert.Equal(artworkYear, artWork.Year);
        }

        [Fact]
        public void GetCategoryByIdShouldReturnsCategory()
        {
            // Arrange

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

            var service = new ArtisticWorkService(dbContext);

            // Act
            var category = service.GetCategoryById(categoryId);

            // Assert
            Assert.Equal(categoryId, category.Id);
            Assert.Equal(categoryName, category.Name);
        }

        [Fact]
        public void AddCategoryShouldAddCategoryCorrect()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_Category_Db")
               .Options;

            var dbContext = new WAGDbContext(options);
            var service = new ArtisticWorkService(dbContext);

            var newCategory = new ArtisticWorkCategory()
            {
                Name = "Add Category - test"
            };

            // Act
            service.AddCategory(newCategory);

            // Assert
            Assert.True(dbContext
                .ArtisticWorkCategories
                .Any(category => category.Name == newCategory.Name));
        }

        [Fact]
        public void DeleteCategoryShouldDeleteCategoryCorrect()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
               .UseInMemoryDatabase(databaseName: "Delete_Category_Db")
               .Options;

            var dbContext = new WAGDbContext(options);
            var service = new ArtisticWorkService(dbContext);

            var categorySeed = new ArtisticWorkCategory()
            {
                Name = "Test Category Name",
            };

            dbContext.ArtisticWorkCategories.Add(categorySeed);
            dbContext.SaveChanges();

            var categoryId = dbContext.ArtisticWorkCategories.LastOrDefault().Id;

            // Act
            service.DeleteCategory(categoryId);

            // Assert
            Assert.False(dbContext
                .ArtisticWorkCategories
                .Any(cat => cat.Id == categoryId));
        }
    }
}
