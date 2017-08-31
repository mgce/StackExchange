namespace StackExchange.Core.Entities
{
    public class Wallet : BaseEntity
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
