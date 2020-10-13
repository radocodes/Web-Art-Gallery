using CloudinaryDotNet;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class EditArtWorkViewModel: BaseModel<int>
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        [Display(Name = "Has Frame")]
        public bool HasFrame { get; set; }

        public int ArtisticWorkCategoryId { get; set; }

        public string Technique { get; set; }

        public string PictureFileName { get; set; }

        public IEnumerable<ArtWorkCategoryViewModel> ExistingCategories { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
