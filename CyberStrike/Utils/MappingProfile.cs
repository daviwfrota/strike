using AutoMapper;
using CyberStrike.Models.DAO;
using CyberStrike.Models.DTO.Clients;
using NetTopologySuite.Geometries;

namespace CyberStrike.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClientDto, Client>();
        CreateMap<Client, ClientDto>()
            .ForMember(dto => dto.Password, opt => opt.Ignore());
        
        CreateMap<ClientLocation, ClientLocationDto>()
            .ForMember(farm => farm.Latitude,
                opt => opt.MapFrom((r, dto) => r.Location.Y))
            .ForMember(farm => farm.Longitude,
                opt => opt.MapFrom((r, dto) => r.Location.X));
        
        CreateMap<ClientLocationDto, ClientLocation>()
            .ForMember(farm => farm.Location,
                opt => opt.MapFrom((dto, r) => r.Location = new Point(dto.Longitude, dto.Latitude)));

        CreateMap<ClientProfile, ClientProfileDto>();
        CreateMap<ClientProfileDto, ClientProfile>();


    }
}