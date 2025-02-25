using AutoMapper;
using UserApi.Dtos.Competences;
using UserApi.Dtos.Files;
using UserApi.Dtos.Users;
using UserApi.Models.Competences;
using UserApi.Models.Files;
using UserApi.Models.Users;

namespace UserApi.AutoMappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserField, UserFieldDto>().ReverseMap();
        CreateMap<Field, FieldDto>().ReverseMap();

        CreateMap<Competence, CompetenceDto>().ReverseMap();

        CreateMap<AppFile, FileResponse>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.FileContentType, opt => opt.MapFrom(src => src.ContentType));
    }
}
