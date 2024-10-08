using User_Service.API.DTOs;
using User_Service.API.Interfaces.Events;
using User_Service.API.Interfaces.Persistence;
using User_Service.API.Interfaces.Services;
using User_Service.API.Models;
using User_Service.API.Models.Validations;

namespace User_Service.API.Services
{
    public class CustomerService(ICustomerRepository customerRepository, IAuthService authService, INotifyer notifyer)
               : Service(notifyer),
                 ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IAuthService _authService = authService;

        public async Task<bool> CreateAsync(Customer customer)
        {
            try
            {
                if (!ExecuteValidation(new CustomerValidation(), customer))
                {
                    Notify("Cliente com credenciais inválidas");
                    return false;
                }
                if (await _customerRepository.CustomerAlreadyExists(customer))
                {
                    Notify("Já existe um cliente com estes dados");
                    return false;
                }

                customer.CryptographyPassword(_authService.ComputeSha256Hash(customer.Password));

                await _customerRepository.CreateCustomerAsync(customer);
                return true;
            }
            catch (Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
                return false;
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
        public async Task<bool> UpdateAsync(UpdateCustomerDTO customerDTO)
        {
            try
            {
                var customer = customerDTO.MapToEntity();

                if (!ExecuteValidation(new CustomerValidation(), customer))
                {
                    Notify("Cliente com credenciais inválidas");
                    return false;
                }

                if (await _customerRepository.CustomerAlreadyExists(customer))
                {
                    Notify("Já existe um cliente com estes dados");
                    return false;
                }

                _customerRepository.UpdateCustomer(customer);

                return true;
            }
            catch (Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SetCustomerAsDeleted(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer is null)
                {
                    Notify("O cliente não existe");
                    return false;
                }

                customer.SetEntityAsDeleted();
                _customerRepository.UpdateCustomer(customer);

                return true;
            }
            catch (Exception ex)
            {
                Notify($"Erro: Ocorreu a exception: {ex.Message}");
                return false;
            }
        }
    }
}
