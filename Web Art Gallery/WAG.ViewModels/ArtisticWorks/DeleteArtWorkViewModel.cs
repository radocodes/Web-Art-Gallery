using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class DeleteArtWorkViewModel : BaseModel<int>
    {
        public int Year { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
