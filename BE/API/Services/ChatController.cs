using System.Security.Claims;
using API.Types.Objects;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Services;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/chat")]
public class ChatController : ControllerBase
{
    private readonly IMessageService _mesSer;
    private readonly IMapper _mapper;

    public ChatController(IMessageService mesSer, IMapper mapper)
    {
        _mesSer = mesSer;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("{convId:int}")]
    public async Task<ActionResult> Create([FromRoute] int convId, [FromBody] CreateMessageReq req)
    {
        var selfIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (selfIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(selfIdString, out int selfId);

        var arg = _mapper.Map<CreateMessageReq, CreateMessageArg>(req, opt =>
            opt.AfterMap((src, des) =>
            {
                des.UserId = selfId;
                des.ConversationId = convId;
            }));

        return Ok(await _mesSer.AddAsync(arg));
    }
}
