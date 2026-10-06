using DPA.SHOPPING.CORE.Core.DTOs;
using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteUserDTO>> GetFavorites();
        Task<FavoriteUserDTO> GetFavoritesByUserId(int userId);
        Task<bool> CreateFavorite(FavoriteCreateDTO favoriteDTO);
        Task<bool> DeleteFavorite(FavoriteDeleteDTO favoriteDTO);
    }
}
