using User_Service.API.Interfaces.Persistence;

namespace User_Service.API.Data.Persistence
{
    public class UnitOfWork(CustomerDbContext context) : IUnitOfWork
    {
        private readonly CustomerDbContext _context = context;

        public async Task<bool> Commit()
        {
            var ret = await _context.SaveChangesAsync();
            if (ret > 0)
                return true;

            return false;
        }
        public async Task<bool> Rollback() => await Task.FromResult(true);
    }
}
