using CloudinaryDotNet;
using System.Collections.Generic;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class ArtWorkCollectionViewModel
    {
        public List<ArtisticWork> ArtWorkCollection { get; set; }

        public ArtisticWorkCategory ArtWorkCategory { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
