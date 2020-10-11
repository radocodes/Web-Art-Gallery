using System.Collections.Generic;
using WAG.Data.Models;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.Services.Interfaces
{
    public interface IArtisticWorkService
    {
        void AddArtWork(ArtisticWork artisticWork);

        void EditArtWork(ArtisticWork artWorkUdate);

        void DeleteArtWork(int id);

        void AddCategory(ArtisticWorkCategory artWorkCategory);

        void EditCategory(ArtisticWorkCategory artWorkCategory);

        void DeleteCategory(int categoryId);

        IEnumerable<ArtisticWorkCategory> GetArtisticWorkCategories();

        IEnumerable<ArtisticWork> GetArtWorksByCategoryId(int id);

        IEnumerable<ArtisticWork> GetFilteredArtWorksByCategoryId(int id, string availability, string price);

        ArtisticWork GetArtisticWorkById(int id);

        ArtisticWorkCategory GetCategoryById(int id);
    }
}
