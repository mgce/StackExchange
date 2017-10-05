using System.Threading.Tasks;
using StackExchange.Core.Entities;

namespace StackExchange.Core.Repositories
{
    public interface IStackPriceRepository : IRepository<StackPrice>
    {
        Task<decimal> GetActualPriceByCompany(long companyId);
    }
}