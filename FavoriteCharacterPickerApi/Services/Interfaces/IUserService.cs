using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;

namespace FavoriteCharacterPickerApi.Services;

public interface IUserService
{
    public Task<UserDto> CreateUser(CreateUserRequest request);
    public Task<UserDto> GetUserById(int id);
    public Task<UserDto> GetUserByName(string name);
    public Task<UserDto> EditUser(int id, EditUserRequest request);
    public Task DeleteUser(int id);
}