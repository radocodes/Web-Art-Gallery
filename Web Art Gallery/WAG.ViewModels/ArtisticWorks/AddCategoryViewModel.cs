using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WAG.ViewModels.ArtisticWorks
{
    public class AddCategoryViewModel
    {
        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }

        public IFormFile Picture { get; set; }
    }
}
