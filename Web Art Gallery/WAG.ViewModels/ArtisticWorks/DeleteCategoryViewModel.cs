using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class DeleteCategoryViewModel
    {
        public ArtisticWorkCategory ArtWorkCategory { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
