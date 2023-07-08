using NHMatsumotoDemo.Domain.Entities;

namespace NHMatsumotoDemo.Services
{
    public interface IEnderecoServices
    {
        Task<int> Create(Endereco endereco);
        Task<Endereco> GetById(int id);
        Task Put(int id, Endereco endereco);
    }
}
