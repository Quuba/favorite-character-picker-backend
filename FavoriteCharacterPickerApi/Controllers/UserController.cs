using FavoriteCharacterPickerApi.Core.Errors;
using FavoriteCharacterPickerApi.Data.Enums;
using FavoriteCharacterPickerApi.Services;
using FavoriteCharacterPickerApi.Transactional.User.Dtos;
using FavoriteCharacterPickerApi.Transactional.User.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteCharacterPickerApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    // [HttpPost]
    // public async Task<IActionResult> PostUserAsync(CreateUserRequest request)
    // {
    //     var baseUrl = ($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api");
    //
    //     var createdUser = await _userService.CreateUser(request);
    //     
    //     var url = baseUrl + $"/{createdUser.Id}";
    //
    //     return CreatedAtRoute(url, createdUser);
    // }
    
    [HttpGet]
    [Route("{id:int}")]
    [Authorize (Roles = "role_admin")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        catch (FcpError e)
        {
            if (e.ErrorType == FcpErrorType.UserNotFound)
            {
                return NotFound();
            }
            Console.WriteLine("ERROR in UserController");
            Console.WriteLine(e);

            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("{name}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUserByName(string name)
    {
        try
        {
            var user = await _userService.GetUserByName(name);
            return Ok(user);
        }
        catch (FcpError e)
        {
            if (e.ErrorType == FcpErrorType.UserNotFound)
            {
                return NotFound();
            }
            Console.WriteLine("ERROR in UserController");
            Console.WriteLine(e);
            throw;
        }
    }

    // [HttpPatch]
    // [Route("{id:int}")]
    // [Authorize]
    // public async Task<ActionResult<UserDto>> EditUser(int id, [FromBody] EditUserRequest request)
    // {
    //     try
    //     {
    //         var editedUser = await _userService.EditUser(id, request);
    //         return Ok(editedUser);
    //     }
    //     catch (FcpError e)
    //     {
    //         if (e.ErrorType == FcpErrorType.UserNotFound)
    //         {
    //             return NotFound();
    //         }
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }

    // [HttpDelete]
    // [Route("{id:int}")]
    // [Authorize]
    // public async Task<IActionResult> DeleteUser(int id)
    // {
    //     await _userService.DeleteUser(id);
    //     return Ok();
    // }
}