using NHMatsumotoDemo.Domain.Entities;
using NHMatsumotoDemo.Domain.Interfaces;
using System.Linq.Expressions;

namespace NHMatsumotoDemo.Infrastructure.Database.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IRepository<Usuario> _usuarioRepository;
        private bool _disposed = false;

        public UsuarioRepository(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Create(Usuario usuario)
          => await _usuarioRepository.Create(usuario);

        public async Task Update(Usuario usuario)
            => await _usuarioRepository.Update(usuario);

        public async Task<Usuario> GetById(int id)
            => await _usuarioRepository.GetById(id);

        public async Task<IEnumerable<Usuario>> GetByExpression(Expression<Func<Usuario, bool>> predicate)
            => await _usuarioRepository.GetByExpression(predicate);

        public async Task<IEnumerable<Usuario>> GetAll()
            => await _usuarioRepository.GetAll();

        public void Dispose()
        {
            if (!_disposed)
                _usuarioRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
