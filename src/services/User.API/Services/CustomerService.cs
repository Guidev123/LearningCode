using Azure.Core;
using User.API.DTOs;
using User.API.Interfaces.Events;
using User.API.Interfaces.Persistence;
using User.API.Interfaces.Services;
using User.API.Models;
using User.API.Models.Validations;

namespace User.API.Services
{
    public class CustomerService(ICustomerRepository customerRepository, IAuthService authService , INotifyer notifyer)
               : Service(notifyer),
                 ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IAuthService _authService = authService;

        public async Task CreateAsync(CreateCustomerDTO customerDTO)
        {
            try
            {
                var customer = customerDTO.MapToEntity();

                if (!ExecuteValidation(new CustomerValidation(), customer))
                {
                    Notify("Cliente com credenciais inválidas");
                    return;
                }
                if (await _customerRepository.CustomerAlreadyExists(customer))
                {
                    Notify("Já existe um cliente com estes dados");
                    return;
                }

                customer.CryptographyPassword(_authService.ComputeSha256Hash(customer.Password));

                await _customerRepository.CreateCustomerAsync(customer);
            }
            catch(Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
            }
        }

        public async Task<string> LoginAsync(LoginCustomerDTO customerDTO)
        {
            try
            {
                var passwordHash = _authService.ComputeSha256Hash(customerDTO.Password);
                var customer = await _customerRepository.GetCustomerByEmailAndPasswordAsync(customerDTO.Email, passwordHash);
                if (customer is null)
                {
                    Notify("Usuario ou senha incorretos");
                    return string.Empty;
                }

                return _authService.GenerateJwtToken(customer.Email.Address, customer.UserType.ToString());
            }
            catch (Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
                return string.Empty;
            }
        }
        public async Task UpdateAsync(UpdateCustomerDTO customerDTO)
        {
            try
            {
                var customer = customerDTO.MapToEntity();

                if (!ExecuteValidation(new CustomerValidation(), customer))
                {
                    Notify("Cliente com credenciais inválidas");
                    return;
                }

                if (await _customerRepository.CustomerAlreadyExists(customer))
                {
                    Notify("Já existe um cliente com estes dados");
                    return;
                }

                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
            }
        }

        public async Task SetCustomerAsDeleted(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if(customer is null)
                {
                    Notify("O cliente não existe");
                    return;
                }

                customer.SetEntityAsDeleted();
                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
            }
        }
    }
}
