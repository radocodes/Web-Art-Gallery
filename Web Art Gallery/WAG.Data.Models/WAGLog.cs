using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class WAGLog : BaseModel<int>
    {
        public string Message { get; set; }

        public int EventId { get; set; }

        public string LogLevel { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
