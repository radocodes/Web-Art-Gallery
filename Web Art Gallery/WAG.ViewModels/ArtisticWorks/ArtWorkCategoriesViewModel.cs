﻿using CloudinaryDotNet;
using System.Collections.Generic;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class ArtWorkCategoriesViewModel
    {
        public List<ArtWorkCategoryViewModel> Categories { get; set; }

        public Cloudinary Cloudinary { get; set; }
    }
}
