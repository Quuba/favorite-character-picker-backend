using System.Security.Cryptography;
using System.Text;
using FavoriteCharacterPickerApi.Core.Errors;
using FavoriteCharacterPickerApi.Data;
using FavoriteCharacterPickerApi.Services.Interfaces;
using FavoriteCharacterPickerApi.Transactional.Auth.Responses;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;
using Microsoft.EntityFrameworkCore;

namespace FavoriteCharacterPickerApi.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _dataContext;
    private readonly IUserService _userService;

    public AuthService(DataContext dataContext, IUserService userService)
    {
        _dataContext = dataContext;
        _userService = userService;
    }

    public async Task<UserDto> Register(RegisterRequest request)
    {
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        if (await _dataContext.Users.AnyAsync(u => u.Username == request.Username))
        {
            Console.WriteLine($"user with username {request.Username} already exists:");
            throw new FcpError(FcpErrorType.UsernameIsTaken);
        }
            
        if (await _dataContext.Users.AnyAsync(u => u.Email == request.Email))
            throw new FcpError(FcpErrorType.UserAlreadyExists);
        
        CreateUserRequest createUserRequest = new CreateUserRequest()
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        return await _userService.CreateUser(createUserRequest);
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var foundUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (foundUser == null)
            throw new FcpError(FcpErrorType.UserNotFound);

        if (!VerifyPasswordHash(request.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
            throw new FcpError(FcpErrorType.BadCredentials);

        LoginResponse response = new LoginResponse()
        {
            Token = "dupa",
            UserData = await _userService.GetUserById(foundUser.Id)
        };
        
        return response;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken()
    {
        return "a";
    }
}