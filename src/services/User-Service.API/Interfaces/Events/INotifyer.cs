using User_Service.API.Events.Notify;

namespace User_Service.API.Interfaces.Events
{
    public interface INotifyer
    {
        void Handle(Notification notification);
        bool HasNotification();
        List<Notification?> GetNotifications();
    }
}
