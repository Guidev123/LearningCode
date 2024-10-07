using User.API.DTOs;
using User.API.Models;

namespace User.API.Interfaces.Services
{
    public interface ICustomerService
    {
        Task CreateAsync(Customer customer);
        Task<string> LoginAsync(LoginCustomerDTO login);
        Task UpdateAsync(Customer customer);
        Task SetCustomerAsDeleted(Guid id);
    }
}
