using System.Security.Claims;
using API.Services;
using API.Types.Objects;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/conversation")]
public class ConversationController : ControllerBase
{
    private readonly IConversationService _convSer;

    public ConversationController(IConversationService convSer)
    {
        _convSer = convSer;
    }

    [HttpGet]
    [Route("{convId:int}")]
    public async Task<ActionResult<ConversationRes>> GetByConvId([FromRoute] int convId)
    {
        var selfIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (selfIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(selfIdString, out int selfId);

        var conv = await _convSer.GetAsync(convId);

        return Ok(conv);
    }

    [HttpGet]
    [Route("self/all")]
    public async Task<ActionResult<IEnumerable<ConversationRes>>> GetByUserId()
    {
        var selfIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (selfIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(selfIdString, out int selfId);

        var convs = await _convSer.GetByUserIdAsync(selfId);

        return Ok(convs);
    }

    [HttpGet]
    [Route("self/user/all")]
    public async Task<ActionResult<IEnumerable<UserRes>>> GetUser()
    {
        var selfIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (selfIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(selfIdString, out int selfId);

        var convs = await _convSer.GetUserAsync(selfId);

        return Ok(convs);
    }

    [HttpPost]
    [Route("{userId:int}")]
    public async Task<ActionResult<string>> Create([FromRoute] int userId)
    {
        var selfIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (selfIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(selfIdString, out int selfId);

        if (userId == selfId)
            return BadRequest(new FailureRes { Message = "Can't create conversation with yourself!" });

        var arg = new CreateConvArg { UserId = userId, SelfId = selfId };

        return Ok(await _convSer.AddAsync(arg));
    }
}
