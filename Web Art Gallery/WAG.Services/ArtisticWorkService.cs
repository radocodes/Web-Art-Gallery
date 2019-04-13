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
using WAG.ViewModels.InputViewModels;

namespace WAG.Services
{
    public class ArtisticWorkService : IArtisticWorkService
    {
        private WAGDbContext DbContext;

        public ArtisticWorkService(WAGDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void AddArtWorkAsync(ArtWorkInputViewModel inputViewModel)
        {
            var category = new ArtisticWorkCategory();

            if (DbContext.ArtisticWorkCategories.Any(x => x.Name == inputViewModel.Category))
            {
                category = DbContext.ArtisticWorkCategories.FirstOrDefault(x => x.Name == inputViewModel.Category);
            }

            else
            {
                category.Name = inputViewModel.Category;
            }

            var technique = new ArtisticWorkTechnique();

            if (DbContext.ArtisticWorkTechniques.Any(x => x.Name == inputViewModel.Technique))
            {
                technique = DbContext.ArtisticWorkTechniques.FirstOrDefault(x => x.Name == inputViewModel.Technique);
            }

            else
            {
                technique.Name = inputViewModel.Technique;
            }

            var order = new Order()
            {
                OrderInfo = "Test"
            };

            var artWork = new ArtisticWork()
            {
                Year = inputViewModel.Year,
                Height = inputViewModel.Height,
                Width = inputViewModel.Width,
                Price = inputViewModel.Price,
                Availability = inputViewModel.Availability,
                HasFrame = inputViewModel.HasFrame,
                ArtisticWorkCategory = category,
                ArtisticWorkTechnique = technique,
                Picture = UploadPictureAsync(inputViewModel.Picture).Result,
            };

            this.DbContext.ArtisticWorks.Add(artWork);

            this.DbContext.SaveChanges();
        }

        private async Task<string> UploadPictureAsync(IFormFile picture)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";

            var filePath = $@"D:\RADO\IT\Projects\Web Art Gallery\Web Art Gallery\WAG WebApp\wwwroot\images\{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            };

            return fileName;
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
    }
}
