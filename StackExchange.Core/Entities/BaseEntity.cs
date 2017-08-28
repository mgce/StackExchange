using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
