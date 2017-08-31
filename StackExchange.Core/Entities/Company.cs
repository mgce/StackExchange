using System.Collections.Generic;
using System.Linq;

namespace StackExchange.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<StackPrice> ActualStackPrice { get; set; }

        public ICollection<Stack> Stacks { get; set; }

        public long StackUnits { get; set; }

        public decimal GetActualStackPrice()
        {
           var price =  ActualStackPrice
                    .SingleOrDefault(x => x.CreatedAt == ActualStackPrice.Max(y => y.CreatedAt));
            return price?.Price ?? 0;

        }
    }
}
