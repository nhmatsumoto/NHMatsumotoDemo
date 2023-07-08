using NHMatsumotoDemo.Domain.Entities;
using NHMatsumotoDemo.Domain.Interfaces;
using System.Linq.Expressions;

namespace NHMatsumotoDemo.Infrastructure.Database.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly IRepository<Endereco> _enderecoRepository;
        private bool _disposed = false;
        public EnderecoRepository(IRepository<Endereco> repository)
        {
            _enderecoRepository = repository;
        }

        public async Task<Endereco> Create(Endereco endereco)
           => await _enderecoRepository.Create(endereco);

        public async Task Update(Endereco endereco)
            => await _enderecoRepository.Update(endereco);

        public async Task<Endereco> GetById(int id)
            => await _enderecoRepository.GetById(id);

        public async Task<IEnumerable<Endereco>> GetByExpression(Expression<Func<Endereco, bool>> predicate)
            => await _enderecoRepository.GetByExpression(predicate);

        public async Task<IEnumerable<Endereco>> GetAll()
            => await _enderecoRepository.GetAll();
        public void Dispose()
        {
            if (!_disposed)
                _enderecoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
