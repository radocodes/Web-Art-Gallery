using System.Collections.Generic;
using WAG.Data.Models;
using WAG.ViewModels.ArtisticWorks;

namespace WAG.Services.Interfaces
{
    public interface IArtisticWorkService
    {
        void AddArtWork(AddArtWorkViewModel inputViewModel);

        void EditArtWork(int id, EditArtWorkViewModel editArtWorkViewModel);

        void DeleteArtWork(int id);

        void AddCategory(AddCategoryViewModel addCategoryViewModel);

        void EditCategory(int CategoryId, EditCategoryInputViewModel editCategoryInputViewModel);

        void DeleteCategory(int categoryId);

        List<ArtisticWorkCategory> GetArtisticWorkCategories();

        List<ArtisticWork> GetArtWorksByCategoryId(int id);

        List<ArtisticWork> GetArtWorksByCategoryIdAndFilter(int id, string availability, string price);

        ArtisticWork GetArtisticWorkById(int id);

        ArtisticWorkCategory GetCategoryById(int id);

        EditArtWorkViewModel GetEditArtWorkViewModel(int id);
    }
}
