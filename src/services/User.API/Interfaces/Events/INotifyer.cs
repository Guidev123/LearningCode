using User.API.Events.Notify;

namespace User.API.Interfaces.Events
{
    public interface INotifyer
    {
        void Handle(Notification notification);
        bool HasNotification();
        List<Notification?> GetNotifications();
    }
}
