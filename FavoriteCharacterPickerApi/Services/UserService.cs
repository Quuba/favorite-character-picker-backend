using FavoriteCharacterPickerApi.Data;
using FavoriteCharacterPickerApi.Data.Entities;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;

namespace FavoriteCharacterPickerApi.Services;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    
    public UserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<UserDto> CreateUser(CreateUserRequest request)
    {
        var user = await _dataContext.Users.AddAsync(new User()
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash =request.Password
        });
        await _dataContext.SaveChangesAsync();

        return new UserDto()
        {
            Id = user.Entity.Id,
            Username = user.Entity.Username
        };
    }

    public async Task GetUserById()
    {
        throw new NotImplementedException();
    }

    public async Task GetUserByName()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUser()
    {
        throw new NotImplementedException();
    }
}