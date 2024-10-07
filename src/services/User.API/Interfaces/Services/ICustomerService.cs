using User.API.DTOs;
using User.API.Models;

namespace User.API.Interfaces.Services
{
    public interface ICustomerService
    {
        Task CreateAsync(CreateCustomerDTO customerDTO);
        Task<string> LoginAsync(LoginCustomerDTO customerDTO);
        Task UpdateAsync(UpdateCustomerDTO customerDTO);
        Task SetCustomerAsDeleted(Guid id);
    }
}
