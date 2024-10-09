using User_Service.API.Enums;
using User_Service.API.ValueObjects;

namespace User_Service.API.Models
{
    public class Customer
    {
        public Customer(string fullName, Email email, string password,
                    Document document, string phone, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            Password = password;
            Document = document;
            Phone = phone;
            UserType = ECustomerType.Customer;
            BirthDate = birthDate;
            IsDeleted = false;
        }
        protected Customer() { } // EF Relation

        public Guid Id { get; }
        public string FullName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public string Password { get; private set; } = string.Empty;
        public Document Document { get; private set; } = null!;
        public ECustomerType UserType { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsDeleted { get; private set; }

        public void SetEntityAsDeleted()
        {
            IsDeleted = true;
        }
        public void CryptographyPassword(string password)
        {
            Password = password;
        }
    }
}
