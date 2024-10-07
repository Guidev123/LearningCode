using User.API.Models;

namespace User.API.Interfaces.Persistence
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetCustomerByIdAsync(Guid id);
        public Task<bool> CustomerAlreadyExists(Customer customer);
        Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string passwordHash);
        public Task CreateCustomerAsync(Customer customer);
        public void UpdateCustomer(Customer customer);
    }
}
