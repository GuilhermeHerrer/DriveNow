using AutoMapper;
using DriveNowApi.Models;
using DriveNowApi.DTOs;

namespace DriveNowApi.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDTO>().ReverseMap();   
    }
}
