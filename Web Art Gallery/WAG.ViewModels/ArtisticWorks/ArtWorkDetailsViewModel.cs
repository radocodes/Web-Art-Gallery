using CloudinaryDotNet;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{ 
   public class ArtWorkDetailsViewModel
    {
        public ArtisticWork ArtisticWork { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
