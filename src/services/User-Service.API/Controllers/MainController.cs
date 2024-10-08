using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Service.API.Events.Notify;
using User_Service.API.Interfaces.Events;

namespace User_Service.API.Controllers
{
    [ApiController]
    public abstract class MainController(INotifyer notifyer) : ControllerBase
    {
        private readonly INotifyer _notifier = notifyer;

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
