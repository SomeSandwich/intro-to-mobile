using System.Security.Claims;
using System.Web;
using Api.Context;
using API.Services;
using API.Types.Objects;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shortid;
using Stump.Storage.Types.Constant;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userSer;
    private readonly IPostService _postSer;
    private readonly IMinioFileService _fileSer;

    private readonly MobileDbContext _context;


    public UserController(IUserService userService, IPostService postSer, IMinioFileService fileSer,
        MobileDbContext context)
    {
        _userSer = userService;
        _postSer = postSer;
        _fileSer = fileSer;
        _context = context;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<UserRes>> GetById([FromRoute] int id)
    {
        var user = await _userSer.Get(id);

        return Ok(user);
    }

    [HttpGet]
    [Route("most-legit")]
    public async Task<ActionResult<IEnumerable<SellerRes>>> GetMostLegit([FromQuery] int number = 10)
    {
        var list = await _userSer.GetMostLegit(number);

        return Ok(list);
    }

    [HttpGet]
    [Route("self")]
    public async Task<ActionResult<UserRes>> GetSelf()
    {
        var selfIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (selfIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(selfIdString, out int selfId);

        var user = await _userSer.Get(selfId);

        return Ok(user);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserReq args)
    {
        return Ok(await _userSer.AddAccountAsync(args));
    }

    [HttpPost]
    [Route("{id:int}/avatar")]
    public async Task<IActionResult> PostAvatar([FromRoute] int id, [FromForm] UserAvatarReq request)
    {
        var key = $"avatar/{ShortId.Generate(GenHashOptions.FileKey)}";

        try
        {
            if (!await _fileSer.UploadSmallFileAsync(
                    new UploadFileDto
                    {
                        Key = key,
                        Stream = request.File.OpenReadStream(),
                        ContentType = request.File.ContentType,
                        Metadata = new Dictionary<string, string>()
                        {
                            { "OldName", HttpUtility.UrlEncode(request.File.FileName) }
                        }
                    }))
            {
                return StatusCode(500, new FailureRes { Message = "Upload file Failed" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        var success = await _userSer.UpdateAvatarAsync(id, key);
        if (!success)
            return BadRequest(new FailureRes { Message = "Not Found User" });

        return Ok(new SuccessRes());
    }

    [HttpPost("{id:int}/money/{amount:int}")]
    public async Task<ActionResult> AddMoney([FromRoute] int id, [FromRoute] int amount)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user is null)
            return BadRequest(new FailureRes { Message = $"Not found userID:{id}" });

        user.Money += amount;

        await _context.SaveChangesAsync();

        return Ok(new SuccessRes());
    }

    [HttpDelete]
    [Route("{id:int}/avatar")]
    public async Task<IActionResult> DeleteAvatar([FromRoute] int id)
    {
        var success = await _userSer.UpdateAvatarAsync(id, null);
        if (!success)
            return BadRequest(new FailureRes { Message = "Not Found User" });

        return Ok(new SuccessRes());
    }
}
