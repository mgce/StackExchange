using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);

        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task RemoveAsync(T entity);

        Task UpdateAsync(T entity);

        Task SaveChangesAsync();
    }
}
