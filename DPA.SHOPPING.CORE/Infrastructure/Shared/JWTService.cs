using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Core.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DPA.SHOPPING.CORE.Infrastructure.Shared
{
    public class JWTService : IJWTService
    {
        public JWTSettings _settings { get; }
        public JWTService(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateJWToken(User user)
        {
            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var sc = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(sc);

            var claims = new[] {
              new Claim(ClaimTypes.Name, (user.FirstName + "" + user.LastName)),
              new Claim(ClaimTypes.GivenName, user.FirstName),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
              new Claim(ClaimTypes.Country, user.Country),
              new Claim(ClaimTypes.Role, user.Type == "A" ? "Admin": "User"),
              new Claim("UserId",user.Id.ToString()),
          };

            var payload = new JwtPayload(
                            _settings.Issuer
                            , _settings.Audience
                            , claims
                            , DateTime.UtcNow
                            , DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes));

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
