using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class ArticlePicture : BaseModel<int>
    {
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
