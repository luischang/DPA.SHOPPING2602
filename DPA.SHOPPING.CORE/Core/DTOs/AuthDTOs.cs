using System;

namespace DPA.SHOPPING.CORE.Core.DTOs
{
    public class SignInRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInResponseDto
    {
        public UserDTO User { get; set; }
        // JWToken se deja vacío por ahora, se implementará después
        public string JWToken { get; set; } = string.Empty;
    }

    public class SignUpRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
