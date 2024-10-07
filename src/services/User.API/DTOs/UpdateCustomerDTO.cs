using User.API.Models;
using User.API.ValueObjects;

namespace User.API.DTOs
{
    public class UpdateCustomerDTO
    {
        public UpdateCustomerDTO(string phone, string email, string password)
        {
            Phone = phone;
            Email = email;
            Password = password;
        }

        public string Phone { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        public Customer MapToEntity() => new(Phone, new Email(Email), Password);
    }
}
