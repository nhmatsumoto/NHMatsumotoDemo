using AutoMapper;
using NHMatsumotoDemo.Domain.Dtos;
using NHMatsumotoDemo.Domain.Entities;

namespace NHMatsumotoDemo.Services.Translators
{
    public class NHMatsumotoDemoProfile : Profile
    {
        public NHMatsumotoDemoProfile()
        {
            CreateMap<Endereco, EnderecoDTO>();
            CreateMap<EnderecoDTO, Endereco>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
