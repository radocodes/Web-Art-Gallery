using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.ViewModels.ArtisticWorks
{
    public class EditCategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string PictureOld { get; set; }
    }
}
