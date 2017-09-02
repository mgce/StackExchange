using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.EF;

namespace StackExchange.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(Context context) : base(context)
        {
        }
    }
}
