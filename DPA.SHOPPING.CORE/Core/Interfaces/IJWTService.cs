using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Settings;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(User user);
    }
}