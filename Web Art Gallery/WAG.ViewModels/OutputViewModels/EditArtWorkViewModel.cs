using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.ViewModels.OutputViewModels
{
    public class EditArtWorkViewModel
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public bool HasFrame { get; set; }
    }
}
