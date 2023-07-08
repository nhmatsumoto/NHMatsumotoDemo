using NHMatsumotoDemo.Domain.Entities;
using NHMatsumotoDemo.Domain.Interfaces;
using NHMatsumotoDemo.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NHMatsumotoDemo.Infrastructure.Database.Repository
{
    public class Repository<T> : IRepository<T>
       where T : EntityBase
    {
        protected readonly MariaDbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(MariaDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            var result = await DbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> GetById(int id)
            => await DbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetByExpression(Expression<Func<T, bool>> predicate = null)
        {
            var query = DbSet.AsQueryable();

            if(predicate is not null)
            {
                query = query
                    .Where(predicate)
                    .AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
            => await DbSet.ToListAsync();

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
