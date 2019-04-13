using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.ViewModels
{
    public class ArtWorkCollectionViewModel
    {
        public List<ArtisticWork> ArtWorkCollection { get; set; }

        public ArtisticWorkCategory ArtWorkCategory { get; set; }
    }
}
