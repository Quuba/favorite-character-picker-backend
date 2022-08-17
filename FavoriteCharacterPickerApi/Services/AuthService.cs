﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FavoriteCharacterPickerApi.Core.Errors;
using FavoriteCharacterPickerApi.Data;
using FavoriteCharacterPickerApi.Data.Entities;
using FavoriteCharacterPickerApi.Services.Interfaces;
using FavoriteCharacterPickerApi.Transactional.Auth.Responses;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace FavoriteCharacterPickerApi.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _dataContext;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthService(DataContext dataContext, IUserService userService, IConfiguration configuration)
    {
        _dataContext = dataContext;
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<UserDto> Register(RegisterRequest request)
    {
        CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);

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
            Token = CreateToken(foundUser),
            UserData = await _userService.GetUserById(foundUser.Id)
        };
        
        return response;
    }

    private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = Encoding.UTF8.GetString(hmac.Key);
            passwordHash = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    private bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(passwordSalt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(Encoding.UTF8.GetBytes(passwordHash));
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}