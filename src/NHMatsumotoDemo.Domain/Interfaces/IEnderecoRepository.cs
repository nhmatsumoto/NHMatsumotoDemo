using NHMatsumotoDemo.Domain.Entities;
using System.Linq.Expressions;

namespace NHMatsumotoDemo.Domain.Interfaces
{
    public interface IEnderecoRepository : IDisposable
    {
        Task<Endereco> Create(Endereco endereco);
        Task Update(Endereco endereco);
        Task<Endereco> GetById(int id);
        Task<IEnumerable<Endereco>> GetByExpression(Expression<Func<Endereco, bool>> predicate);
        Task<IEnumerable<Endereco>> GetAll();
    }
}
