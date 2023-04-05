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
    private readonly IAccountService _accSer;
    private readonly IUserService _userSer;

    private readonly IMapper _mapper;


    public UserController(IAccountService accSer, IUserService userService)
    {
        _accSer = accSer;
        _userSer = userService;

        var config = new MapperConfiguration(opt => { opt.AddProfile<UserProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserReq args)
    {
        try
        {
            Results.Ok(await _userSer.AddAccountAsync(args));
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
