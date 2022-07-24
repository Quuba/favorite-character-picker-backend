using FavoriteCharacterPickerApi.Services;
using FavoriteCharacterPickerApi.Transactional.User.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteCharacterPickerApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateUserRequest request)
    {
        var createdUser = await _userService.CreateUser(request);
        // return CreatedAtRoute("api/user/get/1", createdUser);
        return Ok();
    }
    
    
}