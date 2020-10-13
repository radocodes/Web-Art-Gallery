using CloudinaryDotNet;
using System.Collections.Generic;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class ArtWorkCollectionViewModel
    {
        public IEnumerable<ArtWorkByCategoryViewModel> ArtWorkCollection { get; set; }

        public ArtWorkCategoryViewModel ArtWorkCategory { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
