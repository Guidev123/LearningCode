using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Interfaces.Services;

namespace UserService.API.Controllers
{
    [ApiController]
    public abstract class MainController(INotifier notifier) : ControllerBase
    {
        private readonly INotifier _notifier = notifier;

        protected ActionResult CustomResponse(object? result = null)
        {
            if (IsSuccess())
            {
                return Ok(new
                {
                    Success = true,
                    Data = result
                });
            }

            return BadRequest(new
            {
                Success = false,
                Errors = _notifier.GetNotifications().Select(x => x.Message)
            });
        }

        protected bool IsSuccess() => !_notifier.HasNotification();
        protected void NotifyError(string message) => _notifier.Handle(new(message));
    }
}
