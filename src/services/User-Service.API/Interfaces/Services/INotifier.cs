using User_Service.API.Models;

namespace User_Service.API.Interfaces.Services
{
    public interface INotifier
    {
        void Handle(Notification notification);
        bool HasNotification();
        List<Notification> GetNotifications();
    }
}
