using System.Collections.Generic;

namespace WAG.Data.Models
{
    public class ArtisticWorkCategory : BaseModel<int>
    {
        public ArtisticWorkCategory()
        {
            this.ArtisticWorks = new HashSet<ArtisticWork>();
        }

        public string Name { get; set; }

        public string MainPictureFileName { get; set; }

        public virtual ICollection<ArtisticWork> ArtisticWorks { get; set; }
    }
}
