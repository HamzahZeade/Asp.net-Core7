using Temp.DAL.Interfaces;

namespace Temp.DAL.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get; }
        Task DisposeAsync();
        Task SaveChngesAsync();
        void SaveCahnges();

        Task RollbackAsync();
        Task<int> SaveChangesAsync();
        Task CommitAsync();

    }
}
