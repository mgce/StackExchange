using StackExchange.Core.Exceptions;

namespace StackExchange.Core.Entities
{
    public class Stack : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public Stack(decimal price, int quantity, User user, Company company)
        {
            Create(price, quantity, user, company);
        }

        public void Create(decimal price, int quantity, User user, Company company)
        {
            if (price <= 0)
            {
                throw new DomainException(ErrorCodes.InvalidStackPrice,
                    "Price is not correct");
            }
            if (quantity <= 0)
            {
                throw new DomainException(ErrorCodes.InvalidStackQuantity,
                    "Quantity must have value");
            }
            if (user == null)
            {
                throw new DomainException(ErrorCodes.InvalidUser,
                    "User doesn't exist.");
            }
        }
    }
}
