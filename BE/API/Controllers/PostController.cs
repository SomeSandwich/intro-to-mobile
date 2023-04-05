using System.Security.Claims;
using System.Web;
using Api.Context.Entities;
using API.Services;
using API.Types.Mapping;
using API.Types.Objects;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shortid;
using Stump.Storage.Types.Constant;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/post")]
public class PostController : ControllerBase
{
    private readonly IPostService _postSer;
    private readonly IMinioFileService _fileSer;

    private readonly IMapper _mapper;

    public PostController(IPostService postSer, IMinioFileService fileSer)
    {
        _postSer = postSer;
        _fileSer = fileSer;

        var config = new MapperConfiguration(opt => { opt.AddProfile<PostProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<string>> Create([FromForm] CreatePostReq request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString is null)
        {
            return Unauthorized();
        }
        int.TryParse(userIdString, out int userId);

        request.UserId = userId;

        // Todo: Check file if upload if failed
        var keysSuccess = new List<string>();
        foreach (var file in request.MediaFiles)
        {
            try
            {
                var key = ShortId.Generate(GenHashOptions.FileKey);
                if (await _fileSer.UploadSmallFileAsync(
                        new UploadFileDto
                        {
                            Key = key,
                            Stream = file.OpenReadStream(),
                            ContentType = file.ContentType,
                            Metadata = new Dictionary<string, string>()
                            {
                                { "OldName", HttpUtility.UrlEncode(file.FileName) }
                            }
                        }))
                {
                    keysSuccess.Add(key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        var now = DateTime.Now;

        var post = _mapper.Map<CreatePostReq, Post>(request,
            opt =>
            {
                opt.AfterMap((src, des) =>
                {
                    des.MediaPath = keysSuccess.ToArray();
                    des.CreatedDate = now;
                    des.UpdatedDate = now;
                });
            });

        var postId = await _postSer.AddAsync(post);

        return CreatedAtAction(nameof(GetId), new { id = postId }, new ResSuccess());
    }

    [HttpGet]
    [Route("{sellerId:int}")]
    public async Task<ActionResult> GetAll([FromRoute] int sellerId)
    {
        var listPost = await _postSer.GetByShopIdAsync(sellerId);

        return Ok(listPost);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> GetId([FromRoute] int id)
    {
        var post = await _postSer.GetAsync(id);
        if (post is null)
            return BadRequest(new ResFailure { Message = $"Không tìm thấy bài đăng với ID: {id}" });

        return Ok(post);
    }

    [HttpPatch]
    [Route("{id:int}")]
    public async Task<ActionResult> UpdateInfoAsync([FromRoute] int id, [FromForm] UpdatePostReq)
    {

        return Ok(new ResSuccess());
    }

}
