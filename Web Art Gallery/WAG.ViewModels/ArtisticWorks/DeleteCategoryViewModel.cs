using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class DeleteCategoryViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string MainPictureFileName { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
