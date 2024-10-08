namespace User_Service.API.DTOs
{
    public class LoginCustomerDTO
    {
        public LoginCustomerDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
