using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAG.Data.Models;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.Services.Interfaces
{
    public interface IArtisticWorkService
    {
        void AddArtWorkAsync(ArtWorkInputViewModel inputViewModel);

        void EditArtWork(int id, ArtWorkInputViewModel artWorkInputViewModel);

        void DeleteArtWork(int id);

        List<ArtisticWorkCategory> GetArtisticWorkCategories();

        List<ArtisticWork> GetArtWorksByCategoryId(int id);

        ArtisticWork GetArtisticWorkById(int id);

        ArtisticWorkCategory GetCategoryById(int id);

        EditArtWorkViewModel GetEditArtWorkViewModel(int id);
    }
}
