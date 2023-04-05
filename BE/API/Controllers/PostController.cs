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
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
            return Unauthorized();

        var selfIdStr = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
        if (selfIdStr is null)
            return Unauthorized();

        if (!int.TryParse(selfIdStr, out var userId))
        {
            return Unauthorized();
        }

        request.UserId = userId;

        // Todo: Check file if upload if failed
        var keysSuccess = new List<string>();
        var listFileFail = new List<string>();

        foreach (var file in request.MediaFiles)
        {
            try
            {
                var key = $"post/{ShortId.Generate(GenHashOptions.FileKey)}";
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
                else
                {
                    listFileFail.Add(file.FileName);
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

        // Todo : return 206
        // if(listFileFail.Count <= 0)
        //     return CreatedAtAction(nameof(GetId), new { id = postId }, new ResSuccess());

        return CreatedAtAction(nameof(GetId), new { id = postId }, new ResSuccess());
    }

    [HttpGet]
    [Route("{sellerId:int}")]
    public async Task<ActionResult> GetBySellerId([FromRoute] int sellerId)
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
    public async Task<ActionResult> UpdateInfoAsync([FromRoute] int id, [FromForm] UpdatePostReq req)
    {
        // #Todo
        foreach (var file in req.MediaFilesDelete)
        {
            try
            {
                await _fileSer.DeleteFileAsync(file);
            }
            catch (Exception e)
            {
                req.MediaFilesDelete.Remove(file);
                Console.WriteLine(e);
            }
        }

        var keysSuccess = new List<string>();
        var listFileFail = new List<string>();

        foreach (var file in req.MediaFilesAdd)
        {
            try
            {
                var key = $"post/{ShortId.Generate(GenHashOptions.FileKey)}";

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
                else
                {
                    listFileFail.Add(file.FileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        var args = new UpdatePostArgs
        {
            Caption = req.Caption, Description = req.Description, MediaFilesAdd = keysSuccess, Price = req.Price
        };

        await _postSer.UpdateAsync(id, args);

        return Ok(new ResSuccess());
    }

    [HttpPatch]
    [Route("{id:int}/toggleHide")]
    public async Task<ActionResult> HideToggle([FromRoute] int id)
    {
        if (!await _postSer.ToggleIsHide(id))
        {
            return BadRequest(new ResFailure { Message = $"Toggle ẩn postId:{id} thất bại" });
        }

        return Ok(new ResSuccess());
    }

    [HttpPatch]
    [Route("{id:int}/toggleDelete")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (!await _postSer.DeleteAsync(id))
        {
            return BadRequest(new ResFailure { Message = $"Toggle xoá postId: {id} thất bại" });
        }

        return Ok(new ResSuccess());
    }
}
