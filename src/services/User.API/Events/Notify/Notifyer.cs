using User.API.Interfaces.Events;

namespace User.API.Events.Notify
{
    public class Notifyer : INotifyer
    {
        private List<Notification> _notifications = [];
        public void Handle(Notification notification) => _notifications.Add(notification);
        public List<Notification?> GetNotifications() => _notifications;
        public bool HasNotification() => _notifications.Count() > 0;
    }
}
