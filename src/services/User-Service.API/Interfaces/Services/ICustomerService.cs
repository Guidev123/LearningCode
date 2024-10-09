using User_Service.API.DTOs;
using User_Service.API.Models;

namespace User_Service.API.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateAsync(Customer customer);
        Task<string> LoginAsync(LoginCustomerDTO customerDTO);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
