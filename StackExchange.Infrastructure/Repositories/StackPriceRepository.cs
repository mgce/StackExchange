using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<decimal> GetActualPriceByCompany(long companyId)
        {
            var stackPrice =  await _context.StackPrices
                .OrderByDescending(x=>x.CreatedAt)
                .FirstOrDefaultAsync(x=>x.CompanyId == companyId);
            return stackPrice.Price;
        }
    }
}
