using AutoMapper;
using CyberStrike.Models.DAO;
using CyberStrike.Models.DTO;

namespace CyberStrike.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientDto, Client>();
        CreateMap<Client, ClientDto>()
            .ForMember(dto => dto.Password, opt => opt.Ignore());
    }
}