using AutoMapper;
using DriveNowApi.DTOs;
using DriveNowApi.Models;

namespace DriveNowApi.Profiles
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>()
                .ForMember(dest => dest.NomeAgencia,
               opt => opt.MapFrom(src => src.Agencia.NomeFantasia))
                .ReverseMap();

            CreateMap<VeiculoCreateDTO, Veiculo>().ReverseMap();
        }
    }
}
