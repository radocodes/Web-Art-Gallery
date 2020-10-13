using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{ 
   public class ArtWorkDetailsViewModel : BaseModel<int>
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public bool HasFrame { get; set; }

        public string PictureFileName { get; set; }

        public ArtWorkCategoryViewModel ArtWorkCategory { get; set; }

        public string Technique { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
