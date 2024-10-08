using User_Service.API.Interfaces.Services;
using User_Service.API.Models;

namespace User_Service.API.Services.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications = [];
        public void Handle(Notification notification) => _notifications.Add(notification);
        public List<Notification> GetNotifications() => _notifications;
        public bool HasNotification() => _notifications.Count() > 0;
    }
}
