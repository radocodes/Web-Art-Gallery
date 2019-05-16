﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class AddCategoryViewModel
    {
        public string CategoryName { get; set; }

        public IFormFile Picture { get; set; }
    }
}