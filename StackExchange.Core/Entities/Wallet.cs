using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Entities
{
    public class Wallet
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
