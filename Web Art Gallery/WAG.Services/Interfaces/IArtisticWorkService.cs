using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAG.Data.Models;
using WAG.ViewModels.InputViewModels;

namespace WAG.Services.Interfaces
{
    public interface IArtisticWorkService
    {
        void AddArtWorkAsync(ArtWorkInputViewModel inputViewModel);

        List<ArtisticWorkCategory> GetArtisticWorkCategories();

        List<ArtisticWork> GetArtWorksByCategoryId(int id);

        ArtisticWork GetArtisticWorkById(int id);

        ArtisticWorkCategory GetCategoryById(int id);
    }
}
