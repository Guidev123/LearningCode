using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Service.API.DTOs;
using User_Service.API.Interfaces.Persistence;
using User_Service.API.Interfaces.Services;
using User_Service.API.Models;

namespace User_Service.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController(IUnitOfWork uow,
                                     INotifier notifier,
                                     ICustomerService customerService,
                                     ICustomerRepository customerRepository)
                                   : MainController(notifier)
    {
        private readonly IUnitOfWork _uow = uow;
        private readonly ICustomerService _customerService = customerService;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if(customer is null)
            {
                NotifyError("Este cliente não existe");
                return CustomResponse();
            }

            return CustomResponse(GetCustomerDTO.MapFromEntity(customer));
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(CreateCustomerDTO customerDTO)
        {
            var customer = customerDTO.MapToEntity();
            await _customerService.CreateAsync(customer);

            if (!IsSuccess())
                return CustomResponse(customer);

            await _uow.Commit();
            return CustomResponse(customer.Id);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginCustomerDTO loginCustomerDTO)
        {
            var result = await _customerService.LoginAsync(loginCustomerDTO);

            if (!IsSuccess())
                return CustomResponse();

            return CustomResponse(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);

            if (!IsSuccess())
                return CustomResponse();

            await _uow.Commit();
            return CustomResponse();
        }
    }
}
