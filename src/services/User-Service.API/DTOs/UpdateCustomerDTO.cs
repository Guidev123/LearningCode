using User_Service.API.Models;
using User_Service.API.ValueObjects;

namespace User_Service.API.DTOs
{
    public class UpdateCustomerDTO
    {
        public UpdateCustomerDTO(string phone, string email, string password, string name)
        {
            Phone = phone;
            Email = email;
            Password = password;
            Name = name;
        }
        public string Name { get; private set; }
        public string Phone { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        public Customer MapToEntity() => new(Phone, new Email(Email), Password, Name);
    }
}
