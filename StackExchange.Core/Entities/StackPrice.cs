using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Entities
{
    public class StackPrice : BaseEntity
    {
        public Company Company { get; set; }
        public long CompanyId { get; set; }
        public decimal Price { get; set; }
    }
}
