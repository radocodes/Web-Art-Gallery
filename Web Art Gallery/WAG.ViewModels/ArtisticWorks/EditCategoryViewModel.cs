
using CloudinaryDotNet;
namespace WAG.ViewModels.ArtisticWorks
{
    public class EditCategoryViewModel
    {
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }

        public string PictureFileName { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
