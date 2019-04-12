using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.ViewModels.InputViewModels
{
    public class ArtWorkInputViewModel
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public bool HasFrame { get; set; }

        public string Category { get; set; }

        public string Technique { get; set; }

        public bool IsDeleted { get; set; }

        public IFormFile Picture { get; set; }

        public string PictureUrl { get; set; }
    }
}
