using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DPA.SHOPPING.CORE.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly StoreDbContext _context;

        public FavoriteRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetFavorites()
        {
            var favorites = await _context
                                    .Favorite
                                    .Include(f => f.User)
                                    .Include(f => f.Product)
                                        .ThenInclude(p => p.Category)
                                    .ToListAsync();
            return favorites;
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByUserId(int userId)
        {
            var favorites = await _context
                                    .Favorite
                                    .Include(f => f.User)
                                    .Include(f => f.Product)
                                        .ThenInclude(p => p.Category)
                                    .Where(f => f.UserId == userId)
                                    .ToListAsync();
            return favorites;
        }

        public async Task<Favorite> GetFavoriteById(int id)
        {
            var favorite = await _context
                                    .Favorite
                                    .Include(f => f.User)
                                    .Include(f => f.Product)
                                        .ThenInclude(p => p.Category)
                                    .Where(f => f.Id == id)
                                    .FirstOrDefaultAsync();
            return favorite;
        }

        public async Task<bool> CreateFavorite(Favorite favorite)
        {
            await _context.Favorite.AddAsync(favorite);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteFavorite(int id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite == null)
                return false;

            _context.Favorite.Remove(favorite);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
