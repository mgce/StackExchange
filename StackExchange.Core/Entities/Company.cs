using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public decimal ActualCompanyPrice { get; set; }

        public ICollection<Stack> Stacks { get; set; }

    }
}
