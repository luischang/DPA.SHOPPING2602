using System.Threading.Tasks;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.SHOPPING.CORE.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDbContext _context;

        public UserRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
