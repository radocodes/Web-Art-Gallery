using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WAG.Data.Models;
using WAG.Services.Interfaces;
using WAG.ViewModels.InputViewModels;

namespace WAG.Services
{
    public class ArtisticWorkService :IArtisticWorkService
    {
        public void AddArtWork(ArtWorkInputViewModel inputViewModel)
        {
            var category = new ArtisticWorkCategory()
            {
                Name = inputViewModel.Category
            };

            var technique = new ArtisticWorkTechnique()
            {
                Name = inputViewModel.Technique
            };

            var pictureUrl = new Picture()
            {
                URL = UploadPictureAsync(inputViewModel.Picture).Result
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
                Picture = pictureUrl
            };
        }

        private async Task<string> UploadPictureAsync(IFormFile picture)
        {
            var filePath = $@"D:\RADO\IT\Projects\Web Art Gallery\Web Art Gallery\WAG WebApp\wwwroot\images\{Guid.NewGuid()}.jpg";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            };

            return filePath;
        }

    }
}
