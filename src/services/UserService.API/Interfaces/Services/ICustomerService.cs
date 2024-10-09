using UserService.API.DTOs;
using UserService.API.Models;

namespace UserService.API.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateAsync(Customer customer);
        Task<string> LoginAsync(LoginCustomerDTO customerDTO);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
