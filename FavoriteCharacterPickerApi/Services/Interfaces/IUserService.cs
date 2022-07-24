using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;

namespace FavoriteCharacterPickerApi.Services;

public interface IUserService
{
    public Task<UserDto> CreateUser(CreateUserRequest request);
    public Task GetUserById();
    public Task GetUserByName();
    public Task DeleteUser();
}