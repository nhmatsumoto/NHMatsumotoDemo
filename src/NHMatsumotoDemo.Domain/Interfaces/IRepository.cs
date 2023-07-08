using NHMatsumotoDemo.Domain.Entities;
using System.Linq.Expressions;

namespace NHMatsumotoDemo.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        Task<T> Create(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetByExpression(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
       
       
    }
}
