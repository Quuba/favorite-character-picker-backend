using FavoriteCharacterPickerApi.Transactional.Auth.Responses;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;

namespace FavoriteCharacterPickerApi.Services.Interfaces;

public interface IAuthService
{
    public Task<UserDto> Register(RegisterRequest request);
    public Task<LoginResponse> Login(LoginRequest request);
}