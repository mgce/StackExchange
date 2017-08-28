using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;

namespace StackExchange.Infrastructure.Repositories
{
    public class StackRepository : Repository<Stack>, IStackRepository
    {
        public StackRepository(Context context) : base(context)
        {
        }
    }
}
