using System.Collections.Generic;

namespace StackExchange.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public decimal ActualCompanyPrice { get; set; }

        public ICollection<Stack> Stacks { get; set; }

    }
}
