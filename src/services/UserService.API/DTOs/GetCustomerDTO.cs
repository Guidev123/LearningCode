using UserService.API.Models;

namespace UserService.API.DTOs
{
    public class GetCustomerDTO(string fullName, string phone, string email)
    {
        public string FullName { get; private set; } = fullName;
        public string Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;

        public static GetCustomerDTO MapFromEntity(Customer customer) =>
            new(customer.FullName, customer.Phone, customer.Email.Address);
    }
}
