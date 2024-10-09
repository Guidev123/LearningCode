using UserService.API.Interfaces.Services;
using UserService.API.Models;

namespace UserService.API.Services.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications = [];
        public void Handle(Notification notification) => _notifications.Add(notification);
        public List<Notification> GetNotifications() => _notifications;
        public bool HasNotification() => _notifications.Count > 0;
    }
}
