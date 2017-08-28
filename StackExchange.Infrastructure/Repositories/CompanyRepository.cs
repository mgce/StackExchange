using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;

namespace StackExchange.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(Context context) : base(context)
        {
        }
    }
}
