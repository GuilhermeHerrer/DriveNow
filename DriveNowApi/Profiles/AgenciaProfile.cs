using AutoMapper;
using DriveNowApi.DTOs;
using DriveNowApi.Models;
namespace DriveNowApi.Profiles;


public class AgenciaProfile : Profile
{
    public AgenciaProfile()
    {
        CreateMap<Agencia, AgenciaDTO>().ReverseMap();
        CreateMap<ViaCepResponse, Agencia>().ReverseMap();
    }
}
