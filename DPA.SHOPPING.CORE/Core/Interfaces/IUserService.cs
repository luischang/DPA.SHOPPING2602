using System.Threading.Tasks;
using DPA.SHOPPING.CORE.Core.DTOs;

namespace DPA.SHOPPING.CORE.Core.Interfaces
{
    public interface IUserService
    {
        Task<SignInResponseDto> SignInAsync(SignInRequestDto dto);
        Task<UserDTO> SignUpAsync(SignUpRequestDto dto);
    }
}
