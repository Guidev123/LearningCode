using Microsoft.EntityFrameworkCore;
using User_Service.API.Models;
using User_Service.API.Interfaces.Persistence;

namespace User_Service.API.Data.Persistence.Repositories
{
    public class CustomerRepository(CustomerDbContext context) : ICustomerRepository
    {
        private readonly CustomerDbContext _context = context;
        public async Task CreateCustomerAsync(Customer customer) => await _context.Customers.AddAsync(customer);

        public async Task<bool> CustomerAlreadyExists(Customer customer)
        {
            var exists = await _context.Customers.CountAsync(x => x.Email.Address == customer.Email.Address);
            if (exists > 0) return true;

            return false;
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id) =>
            await _context.Customers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public void UpdateCustomer(Customer customer) => _context.Customers.Update(customer);

        public async Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string passwordHash)
            => await _context.Customers.SingleOrDefaultAsync(x => x.Email.Address == email && x.Password == passwordHash);
    }
}
