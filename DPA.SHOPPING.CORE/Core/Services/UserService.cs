using System.Threading.Tasks;
using DPA.SHOPPING.CORE.Core.DTOs;
using DPA.SHOPPING.CORE.Core.Entities;
using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Core.Security;

namespace DPA.SHOPPING.CORE.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;

        public UserService(IUserRepository userRepository, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<SignInResponseDto> SignInAsync(SignInRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !PasswordHasher.Verify(user.Password ?? string.Empty, dto.Password))
            {
                return new SignInResponseDto { User = null, JWToken = string.Empty };
            }

            var userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country,
                Address = user.Address,
                Email = user.Email
            };

            return new SignInResponseDto
            {
                User = userDto,
                JWToken = _jwtService.GenerateJWToken(user)
            };
        }

        public async Task<UserDTO> SignUpAsync(SignUpRequestDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                return null!; // caller should handle null (email already exists)
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Country = dto.Country,
                Address = dto.Address,
                Email = dto.Email,
                Password = PasswordHasher.Hash(dto.Password),
                IsActive = true,
                Type = "C"
            };

            var created = await _userRepository.CreateAsync(user);

            return new UserDTO
            {
                Id = created.Id,
                FirstName = created.FirstName,
                LastName = created.LastName,
                DateOfBirth = created.DateOfBirth,
                Country = created.Country,
                Address = created.Address,
                Email = created.Email
            };
        }
    }
}
