using System.Collections.Generic;

namespace StackExchange.Core.Entities
{
    public class Wallet : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public decimal Money { get; set; }
        public ICollection<Stack> Stacks { get; set; }
    }
}
