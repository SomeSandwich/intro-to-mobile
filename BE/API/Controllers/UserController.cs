using API.Services;
using API.Types.Mapping;
using API.Types.Objects;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/user")]
public class UserController : ControllerBase
{
<<<<<<< HEAD
    private readonly IAccountService _accSer;
    private readonly IUserService _userSer;
=======
    private readonly IUserService _userService;
>>>>>>> 3910f3b50c084e1f07714a5924340b4dbc205949

    private readonly IMapper _mapper;


<<<<<<< HEAD
    public UserController(IAccountService accSer, IUserService userService)
    {
        _accSer = accSer;
        _userSer = userService;
=======
    public UserController(IUserService userService)
    {
        _userService = userService;
>>>>>>> 3910f3b50c084e1f07714a5924340b4dbc205949

        var config = new MapperConfiguration(opt => { opt.AddProfile<UserProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserReq args)
    {
        try
        {
            Results.Ok(await _userService.AddAccountAsync(args));
        }
        catch (Exception ex)
        {
            throw;
        }

        return Ok();
    }

    [HttpGet]
    [Route("mostLegit")]
    public async Task<ActionResult<IEnumerable<SellerRes>>> GetMostLegit([FromBody] int number)
    {
        var list = await _userSer.GetMostLegit(number);

        return Ok(list);
    }
}
