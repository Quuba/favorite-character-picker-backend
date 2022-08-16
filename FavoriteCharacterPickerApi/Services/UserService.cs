using AutoMapper;
using FavoriteCharacterPickerApi.Core.Errors;
using FavoriteCharacterPickerApi.Data;
using FavoriteCharacterPickerApi.Data.Entities;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;
using Microsoft.EntityFrameworkCore;

namespace FavoriteCharacterPickerApi.Services;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public UserService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateUser(CreateUserRequest request)
    {
        var user = await _dataContext.Users.AddAsync(new User()
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            PasswordSalt = request.PasswordSalt
        });
        await _dataContext.SaveChangesAsync();

        var mapperUser = _mapper.Map<UserDto>(user.Entity);
        return mapperUser;
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var foundUser = await _dataContext.Users.SingleOrDefaultAsync(user => user.Id == id);
        if (foundUser != null)
        {
            return _mapper.Map<UserDto>(foundUser);
        }
        else
        {
            throw new FcpError(FcpErrorType.UserNotFound);
        }
    }

    public async Task<UserDto> GetUserByName(string name)
    {
        var foundUser = await _dataContext.Users.SingleOrDefaultAsync(user => user.Username == name);
        if (foundUser != null)
        {
            return _mapper.Map<UserDto>(foundUser);
        }
        else
        {
            throw new FcpError(FcpErrorType.UserNotFound);
        }
    }

    public async Task<UserDto> EditUser(int id, EditUserRequest request)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(user => user.Id == id);
        if (user == null)
        {
            throw new FcpError(FcpErrorType.UserNotFound);
        }

        if (request.Username != null)
        {
            user.Username = request.Username;
        }

        await _dataContext.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUser(int id)
    {
        var userToRemove = await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (userToRemove != null)
        {
            _dataContext.Users.Remove(userToRemove);
            await _dataContext.SaveChangesAsync();
        }
        else
        {
            //User not found error
            throw new FcpError(FcpErrorType.UserNotFound);
        }

    }
}