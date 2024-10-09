using UserService.API.Models;

namespace UserService.API.Interfaces.Services
{
    public interface INotifier
    {
        void Handle(Notification notification);
        bool HasNotification();
        List<Notification> GetNotifications();
    }
}
