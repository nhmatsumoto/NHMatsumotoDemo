using NHMatsumotoDemo.Domain.Interfaces;
using NHMatsumotoDemo.Infrastructure.Database.Context;

namespace NHMatsumotoDemo.Infrastructure.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MariaDbContext _context;
        public UnitOfWork(MariaDbContext context)
            => _context = context;

        public async Task<bool> Commit()
            => (await _context.SaveChangesAsync()) > 0;

        public void Dispose()
            => _context.Dispose();

        public Task Rollback()
            => Task.CompletedTask;
    }
}
