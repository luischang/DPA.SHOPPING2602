using System;

namespace DPA.SHOPPING.CORE.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        // Removed IsActive and Type as they are not relevant for favorite queries
    }
}
