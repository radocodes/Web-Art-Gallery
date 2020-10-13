using CloudinaryDotNet;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class AddArtWorkViewModel
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        [Display(Name = "Has Frame")]
        public bool HasFrame { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        public string Technique { get; set; }

        [Required]
        public string PictureFileName { get; set; }

        public IEnumerable<ArtWorkCategoryViewModel> ExistingCategories { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
