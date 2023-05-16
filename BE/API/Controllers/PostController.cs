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
    private readonly IUserService _userSer;
    private readonly ICommentService _commentSer;
    private readonly IMinioFileService _fileSer;

    private readonly IMapper _mapper;

    public PostController(IPostService postSer, IUserService userSer, IMinioFileService fileSer)
    {
        _postSer = postSer;
        _userSer = userSer;
        _fileSer = fileSer;

        var config = new MapperConfiguration(opt => { opt.AddProfile<PostProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> GetId([FromRoute] int id)
    {
        var post = await _postSer.GetAsync(id);
        if (post is null)
            return BadRequest(new FailureRes { Message = $"Không tìm thấy bài đăng với ID: {id}" });

        return Ok(post);
    }

    [HttpGet]
    [Route("seller/{sellerId:int}")]
    public async Task<ActionResult> GetBySellerId([FromRoute] int sellerId)
    {
        var listPost = await _postSer.GetByShopIdAsync(sellerId);

        return Ok(listPost);
    }


    [HttpGet]
    [Route("newest")]
    public async Task<ActionResult<PostRes>> GetListLatest([FromQuery] int number = 10)
    {
        var list = await _postSer.GetLatestAsync(number);

        return Ok(list);
    }

    [HttpGet]
    [Route("category/{categoryId:int}")]
    public async Task<ActionResult> GetListByCategory([FromRoute] int categoryId, [FromQuery] int number = 10,
        [FromQuery] string sort = "inc")
    {
        var list = await _postSer.GetByCategoryAsync(categoryId, number, sort);

        return Ok(list);
    }

    [HttpPost]
    [Route("")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult<string>> Create([FromForm] CreatePostReq request)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
            return Unauthorized();

        var selfIdStr = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
        if (selfIdStr is null)
            return Unauthorized();

        if (!int.TryParse(selfIdStr, out var userId))
        {
            return Unauthorized();
        }

        Console.WriteLine(request.UserId);
        Console.WriteLine(request.CategoryId);
        Console.WriteLine(request.Price);
        Console.WriteLine(request.Caption);
        Console.WriteLine(request.Description);
        Console.WriteLine(request.MediaFiles?.Count);


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
                    des.UserId = userId;
                    des.MediaPath = keysSuccess.ToArray();
                    des.CreatedDate = now;
                    des.UpdatedDate = now;
                });
            });

        var postId = await _postSer.AddAsync(post);

        // Todo : return 206
        // if(listFileFail.Count <= 0)
        //     return CreatedAtAction(nameof(GetId), new { id = postId }, new ResSuccess());

        return CreatedAtAction(nameof(GetId), new { id = postId }, new SuccessRes());
    }

    [HttpPost]
    [Route("{postId:int}/share-by/{userId:int}")]
    public async Task<IActionResult> AddShareBy([FromRoute] int postId, [FromRoute] int userId)
    {
        var userExist = await _userSer.IsUserExist(userId);
        if (!userExist)
            return BadRequest(new FailureRes { Message = "Not Found User" });

        var success = await _postSer.AddSharePost(postId, userId);
        if (!success)
            return BadRequest(new FailureRes { Message = "Not Found Post" });

        return Ok(new SuccessRes());
    }

    [HttpDelete]
    [Route("{postId:int}/share-by/{userId:int}")]
    public async Task<IActionResult> RemoveShareBy([FromRoute] int postId, [FromRoute] int userId)
    {
        var userExist = await _userSer.IsUserExist(userId);
        if (!userExist)
            return BadRequest(new FailureRes { Message = "Not Found User" });

        var success = await _postSer.RemoveSharePost(postId, userId);
        if (!success)
            return BadRequest(new FailureRes { Message = "Not Found Post" });

        return Ok(new SuccessRes());
    }

    [HttpPost]
    [Route("{id:int}/comment")]
    public async Task<IActionResult> AddComment([FromRoute] int id, [FromBody] CommentPostReq request)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
            return Unauthorized();

        var selfIdStr = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
        if (selfIdStr is null)
            return Unauthorized();

        if (!int.TryParse(selfIdStr, out var userId))
        {
            return Unauthorized();
        }

        var success = await _commentSer.AddComment(new CreateCommentReq
        {
            UserId = userId, PostId = id, Content = request.Content, CreateAt = DateTime.Now
        });

        return !success ? StatusCode(500, new FailureRes()) : Ok(new SuccessRes());
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

        return Ok(new SuccessRes());
    }

    [HttpPatch]
    [Route("{id:int}/toggle-hide")]
    public async Task<ActionResult> HideToggle([FromRoute] int id)
    {
        if (!await _postSer.ToggleIsHide(id))
        {
            return BadRequest(new FailureRes { Message = $"Toggle ẩn postId:{id} thất bại" });
        }

        return Ok(new SuccessRes());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (!await _postSer.DeleteAsync(id))
        {
            return BadRequest(new FailureRes { Message = $"Xoá postId: {id} thất bại" });
        }

        return Ok(new SuccessRes());
    }
}
