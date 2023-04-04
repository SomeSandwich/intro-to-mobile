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

    private readonly IMapper _mapper;


    public UserController(IAccountService accSer)
    {
        _accSer = accSer;

        var config = new MapperConfiguration(opt => { opt.AddProfile<UserProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserReq args)
    {
        try
        {
            Results.Ok(await _accSer.AddAccountAsync(args));
        }
        catch (Exception ex)
        {
            throw;
        }

        return Ok();
    }
}