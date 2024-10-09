using UserService.API.Interfaces.Services;
using UserService.API.Models;

namespace UserService.API.Services.Notification
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications = [];
        public void Handle(Notification notification) => _notifications.Add(notification);
        public List<Notification> GetNotifications() => _notifications;
        public bool HasNotification() => _notifications.Count() > 0;
    }
}
