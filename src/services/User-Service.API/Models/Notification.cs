namespace User_Service.API.Models
{
    public class Notification(string message)
    {
        public string Message { get; } = message;
    }
}
