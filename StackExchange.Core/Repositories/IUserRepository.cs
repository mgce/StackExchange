using StackExchange.Core.Entities;
using System.Threading.Tasks;

namespace StackExchange.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsernameAsync(string username);
    }
}
