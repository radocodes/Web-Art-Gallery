using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class ArtisticWorkTechnique : BaseModel<int>
    {
        public ArtisticWorkTechnique()
        {
            this.ArtisticWorks = new HashSet<ArtisticWork>();
        }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ArtisticWork> ArtisticWorks { get; set; }
    }
}
