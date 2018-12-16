using System;
using System.Collections.Generic;
using System.Text;

namespace WAG.Data.Models
{
    public class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
