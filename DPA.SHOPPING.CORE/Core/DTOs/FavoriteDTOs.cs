using System.Collections.Generic;

namespace DPA.SHOPPING.CORE.Core.DTOs
{
    public class FavoriteUserDTO
    {
        public UserDTO User { get; set; }
        public IEnumerable<ProductListDTO> Product { get; set; }
    }

    public class FavoriteCreateDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }

    public class FavoriteDeleteDTO
    {
        public int Id { get; set; }
    }
}
