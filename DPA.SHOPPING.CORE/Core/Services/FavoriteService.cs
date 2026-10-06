using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPA.SHOPPING.CORE.Core.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<FavoriteUserDTO>> GetFavorites()
        {
            var favorites = await _favoriteRepository.GetFavorites();
            var grouped = favorites
                .Where(f => f.User != null)
                .GroupBy(f => f.UserId)
                .Select(g => new FavoriteUserDTO
                {
                    User = new UserDTO
                    {
                        Id = g.First().User.Id,
                        FirstName = g.First().User.FirstName,
                        LastName = g.First().User.LastName,
                        DateOfBirth = g.First().User.DateOfBirth,
                        Country = g.First().User.Country,
                        Address = g.First().User.Address,
                        Email = g.First().User.Email
                    },
                    Product = g
                        .Where(x => x.Product != null)
                        .Select(x => new ProductListDTO
                        {
                            Id = x.Product.Id,
                            Description = x.Product.Description,
                            Price = x.Product.Price,
                            Stock = x.Product.Stock,
                            ImageUrl = x.Product.ImageUrl,
                            Category = x.Product.Category == null ? null : new CategoryListDTO
                            {
                                Id = x.Product.Category.Id,
                                Description = x.Product.Category.Description
                            }
                        })
                        .ToList()
                });

            return grouped;
        }

        public async Task<FavoriteUserDTO> GetFavoritesByUserId(int userId)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserId(userId);
            if (favorites == null || !favorites.Any())
                return null;

            var first = favorites.FirstOrDefault(f => f.User != null);

            return new FavoriteUserDTO
            {
                User = new UserDTO
                {
                    Id = first.User.Id,
                    FirstName = first.User.FirstName,
                    LastName = first.User.LastName,
                    DateOfBirth = first.User.DateOfBirth,
                    Country = first.User.Country,
                    Address = first.User.Address,
                    Email = first.User.Email
                },
                Product = favorites
                    .Where(x => x.Product != null)
                    .Select(x => new ProductListDTO
                    {
                        Id = x.Product.Id,
                        Description = x.Product.Description,
                        Price = x.Product.Price,
                        Stock = x.Product.Stock,
                        ImageUrl = x.Product.ImageUrl,
                        Category = x.Product.Category == null ? null : new CategoryListDTO
                        {
                            Id = x.Product.Category.Id,
                            Description = x.Product.Category.Description
                        }
                    })
                    .ToList()
            };
        }

        public async Task<bool> CreateFavorite(FavoriteCreateDTO favoriteDTO)
        {
            var favorite = new Favorite
            {
                UserId = favoriteDTO.UserId,
                ProductId = favoriteDTO.ProductId,
                CreatedAt = DateTime.UtcNow
            };

            return await _favoriteRepository.CreateFavorite(favorite);
        }

        public async Task<bool> DeleteFavorite(FavoriteDeleteDTO favoriteDTO)
        {
            return await _favoriteRepository.DeleteFavorite(favoriteDTO.Id);
        }
    }
}
