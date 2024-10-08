using User_Service.API.Models;
using User_Service.API.ValueObjects;

namespace User_Service.API.DTOs
{
    public class UpdateCustomerDTO(string phone, string email, string password, string name)
    {
        public string Name { get; private set; } = name;
        public string Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;
        public string Password { get; private set; } = password;

        public Customer MapToEntity() => new(Phone, new Email(Email), Password, Name);
    }
}
