namespace NHMatsumotoDemo.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        void Dispose();
        Task Rollback();
    }
}
