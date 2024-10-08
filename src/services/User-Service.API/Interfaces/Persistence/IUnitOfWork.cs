namespace User_Service.API.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}
