using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Service.API.DTOs;
using User_Service.API.Interfaces.Events;
using User_Service.API.Interfaces.Persistence;
using User_Service.API.Interfaces.Services;

namespace User_Service.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : MainController
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomerService _customerService;
        public CustomersController(IUnitOfWork uow, INotifyer notifyer,
                                   ICustomerService customerService) : base(notifyer)
        {
            _uow = uow;
            _customerService = customerService;
        }

        [HttpPost("create-user")]
        public async Task<ActionResult> CreateAsync(CreateCustomerDTO customerDTO)
        {
            var customer = customerDTO.MapToEntity();
            await _customerService.CreateAsync(customer);

            if (!IsSuccess())
                return CustomResponse(customer);

            await _uow.Commit();
            return CustomResponse(customer.Id);
        }
    }
}
