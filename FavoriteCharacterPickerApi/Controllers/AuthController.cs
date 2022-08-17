using System.Security.Cryptography;
using System.Text;
using FavoriteCharacterPickerApi.Core.Errors;
using FavoriteCharacterPickerApi.Services;
using FavoriteCharacterPickerApi.Services.Interfaces;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteCharacterPickerApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterRequest request)
        {
            try
            {
                var userData = await _authService.Register(request);
                return Ok(userData);
            }
            catch (FcpError e)
            {
                switch (e.ErrorType)
                {
                    case FcpErrorType.UsernameIsTaken:
                        return Conflict();
                    case FcpErrorType.UserAlreadyExists:
                        return Conflict();
                    default:
                        Console.WriteLine($"ERROR: {e.Message}");
                        return Problem();
                }
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(LoginRequest request)
        {
            try
            {
                var response = await _authService.Login(request);
                return Ok(response);
            }
            catch (FcpError e)
            {
                switch (e.ErrorType)
                {
                    case FcpErrorType.UserNotFound:
                        return NotFound();
                    case FcpErrorType.BadCredentials:
                        return Unauthorized();
                    default:
                        Console.WriteLine($"ERROR: {e.Message}");
                        return Problem();
                }
            }
        }
    }
}