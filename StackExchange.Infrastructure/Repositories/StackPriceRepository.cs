using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.EF;

namespace StackExchange.Infrastructure.Repositories
{
    public class StackPriceRepository : Repository<StackPrice>, IStackPriceRepository
    {
        public StackPriceRepository(Context context) : base(context)
        {
        }
    }
}
