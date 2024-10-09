using User_Service.API.ValueObjects;
using UserService.API.Models;

namespace UserService.API.DTOs
{
    public class CreateCustomerDTO(string fullName, string phone, string document, string email, string password, DateTime birthDate)
    {
        public string FullName { get; private set; } = fullName;
        public string Phone { get; private set; } = phone;
        public string Document { get; private set; } = document;
        public string Email { get; private set; } = email;
        public string Password { get; private set; } = password;
        public DateTime BirthDate { get; private set; } = birthDate;

        public Customer MapToEntity() => new(FullName,
                                            new Email(Email),
                                            Password,
                                            new Document(Document),
                                            Phone,
                                            BirthDate);
    }

}
