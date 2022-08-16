using FavoriteCharacterPickerApi.Transactional.User.Dtos;

namespace FavoriteCharacterPickerApi.Transactional.Auth.Responses;

public class LoginResponse
{
    public string Token { get; set; }
    public UserDto UserData { get; set; }
}