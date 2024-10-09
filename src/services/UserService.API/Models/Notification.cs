namespace UserService.API.Models
{
    public class Notification(string message)
    {
        public string Message { get; } = message;
    }
}
