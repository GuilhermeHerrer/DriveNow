using AutoMapper;
using DriveNowApi.DTOs;
using DriveNowApi.Models;

namespace DriveNowApi.Profiles
{
    public class LocacaoProfile : Profile
    {
        public LocacaoProfile()
        {
            CreateMap<Locacao, LocacaoDTO>()
                 .ForMember(dest => dest.NomeVeiculo, opt => opt.MapFrom(src => src.Veiculo.Nome))
                 .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente.Nome));

            CreateMap<LocacaoCreateDTO, Locacao>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.VeiculoId, opt => opt.MapFrom(src => src.IdVeiculo));
        }
    }
}
