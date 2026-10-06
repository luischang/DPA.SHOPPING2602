using DPA.SHOPPING.CORE.Core.Entities;
using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetFavorites();
        Task<IEnumerable<Favorite>> GetFavoritesByUserId(int userId);
        Task<Favorite> GetFavoriteById(int id);
        Task<bool> CreateFavorite(Favorite favorite);
        Task<bool> DeleteFavorite(int id);
    }
}
