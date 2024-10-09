namespace UserService.API.DTOs
{
    public class LoginCustomerDTO(string email, string password)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
