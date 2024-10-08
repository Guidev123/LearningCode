using User_Service.API.Models;
using User_Service.API.ValueObjects;

namespace User_Service.API.DTOs
{
    public class CreateCustomerDTO
    {
        public CreateCustomerDTO(string fullName, string phone, string document, string email, string password, DateTime birthDate)
        {
            FullName = fullName;
            Phone = phone;
            Document = document;
            Email = email;
            Password = password;
            BirthDate = birthDate;
        }

        public string FullName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Document { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public DateTime BirthDate { get; private set; }

        public Customer MapToEntity() => new(FullName,
                                            new Email(Email),
                                            Password,
                                            new Document(Document),
                                            Phone,
                                            BirthDate);
    }

}
