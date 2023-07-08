using NHMatsumotoDemo.Domain.Entities;
using System.Linq.Expressions;

namespace NHMatsumotoDemo.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Create(Usuario usuario);
        Task Update(Usuario usuario);
        Task<Usuario> GetById(int id);
        Task<IEnumerable<Usuario>> GetByExpression(Expression<Func<Usuario, bool>> predicate);
        Task<IEnumerable<Usuario>> GetAll();

    }
}
