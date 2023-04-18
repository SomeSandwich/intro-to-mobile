using API.Services;
using API.Types.Mapping;
using API.Types.Objects;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Tags("Authentication")]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accSer;

    private readonly IMapper _mapper;

    public AuthController(IAccountService accSer, IMapper mapper)
    {
        _accSer = accSer;

        _mapper = mapper;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginRes>> Login([FromBody] LoginReq req)
    {
        var account = await _accSer.LoginAsync(req);
        var remoteIpAddress = HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
        var ipv4 = "";
        if (remoteIpAddress != null) ipv4 = remoteIpAddress.MapToIPv4().ToString();

        var result = await _accSer.GenerateTokenAsync(account, ipv4);
        return Ok(result);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult<LoginRes>> RefreshToken(
        // IConfiguration configuration, HttpRequest request,
        string tokenExpired)
    {
        try
        {
            var remoteIpAddress = HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            string ipv4 = "";
            if (remoteIpAddress != null) ipv4 = remoteIpAddress.MapToIPv4().ToString();
            var result = await _accSer.RefreshTokenAsync(tokenExpired, ipv4);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
