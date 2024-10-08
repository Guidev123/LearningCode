using System.Text.RegularExpressions;
using User_Service.API.Exceptions;

namespace User_Service.API.ValueObjects
{
    public class Email : ValueObject
    {
        protected Email() { } //EF Relation
        public Email(string address)
        {
            if (string.IsNullOrEmpty(address) || address.Length < 5)
                throw new InvalidEmailException();

            Address = address.ToLower().Trim();
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            if (!Regex.IsMatch(address, pattern))
                throw new InvalidEmailException();
        }

        public string Address { get; } = string.Empty;
    }
}
