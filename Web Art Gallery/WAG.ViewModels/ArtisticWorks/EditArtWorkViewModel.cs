using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class EditArtWorkViewModel
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        [Display(Name = "Has Frame")]
        public bool HasFrame { get; set; }

        public int CategoryId { get; set; }

        public string Technique { get; set; }

        public string PictureUrl { get; set; }

        public List<ArtisticWorkCategory> ExistingCategories { get; set; }
    }
}
