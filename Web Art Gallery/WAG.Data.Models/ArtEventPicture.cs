using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class ArtEventPicture : BaseModel<int>
    {
        public int ArtEventId { get; set; }

        public virtual ArtEvent ArtEvent { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
