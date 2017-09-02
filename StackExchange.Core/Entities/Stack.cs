using System;
using StackExchange.Core.Exceptions;

namespace StackExchange.Core.Entities
{
    public class Stack : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public long WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        private Stack()
        {}

        public Stack(decimal price, int quantity, Wallet wallet, Company company)
        {
            Create(price, quantity, wallet, company);
        }

        public Stack Create(decimal price, int quantity, Wallet wallet, Company company)
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
            if (wallet == null)
            {
                throw new DomainException(ErrorCodes.InvalidUser,
                    "User doesn't exist.");
            }

            return new Stack()
            {
                Company = company,
                Quantity = quantity,
                Wallet = wallet
            };
        }
    }
}
