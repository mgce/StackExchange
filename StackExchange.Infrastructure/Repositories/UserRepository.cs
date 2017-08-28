using System.Data.Entity;
using System.Threading.Tasks;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;

namespace StackExchange.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
            : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Set<User>()
                .SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
