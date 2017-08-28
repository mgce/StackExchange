namespace StackExchange.Core.Entities
{
    public class Wallet
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
