using System.Threading.Tasks;
using DPA.SHOPPING.CORE.Core.Entities;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
