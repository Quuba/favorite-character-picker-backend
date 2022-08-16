using AutoMapper;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;

namespace FavoriteCharacterPickerApi.Transactional.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, Data.Entities.User>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => $"{src.Id}")
            )
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => $"{src.Username}")
            ).ReverseMap();
    }
}