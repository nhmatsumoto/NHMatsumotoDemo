using AutoMapper;
using NHMatsumotoDemo.Domain.Entities;
using NHMatsumotoDemo.Domain.Interfaces;

namespace NHMatsumotoDemo.Services
{
    public class EnderecoServices : IEnderecoServices
    {
        protected readonly IEnderecoRepository _repository;
        protected readonly IMapper _mapper;

        public EnderecoServices(IEnderecoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(Endereco endereco)  
        {
            //Regras para cadastro de endereco

            await _repository.Create(endereco);

            return endereco.Id;
        }

        public async Task<Endereco> GetById(int id)
            => await _repository.GetById(id);

        public async Task Put(int id, Endereco endereco)
        {
            var result = await _repository.GetById(id);

            if (endereco is not null)
                await _repository.Update(endereco);

            throw new Exception("Endereço não localizado");
        }
    }
}
