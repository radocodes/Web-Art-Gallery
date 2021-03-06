﻿using System;

namespace WAG.Data.Models
{
    public class Comment : BaseModel<int>
    {
        public string TextBody { get; set; }

        public DateTime Date { get; set; }

        public string WAGUserId { get; set; }

        public virtual WAGUser WAGUser { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
