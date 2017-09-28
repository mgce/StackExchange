using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.EF;

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
                .SingleOrDefaultAsync(u => u.UserName == username);
        }
    }
}
