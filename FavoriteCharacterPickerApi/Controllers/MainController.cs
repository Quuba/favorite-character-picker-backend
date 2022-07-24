using FavoriteCharacterPickerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteCharacterPickerApi.Controllers;

[ApiController]
[Route("api")]
public class MainController : ControllerBase
{
    private readonly IUserService _userService;

    public MainController(IUserService userService)
    {
        _userService = userService;
    }


}