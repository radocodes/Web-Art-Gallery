using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.Data.Models
{
    public class ArtisticWork : BaseModel<int>
    {
        public int Year { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public decimal Price { get; set; }

        public bool Availability { get; set; }

        public bool HasFrame { get; set; }

        public string Picture { get; set; }

        public int ArtisticWorkCategoryId { get; set; }

        public virtual ArtisticWorkCategory ArtisticWorkCategory { get; set; }

        public string Technique { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime EditedOn { get; set; }

        public bool IsDeleted { get; set; }

    }
}
