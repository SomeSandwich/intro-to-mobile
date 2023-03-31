using Api.Modules.GlobalTypes;
using Api.Modules.Post.Mapping;
using Api.Modules.Post.Services;
using Api.Modules.Post.Type;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Post;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    // private readonly MinioClient _minioClient;

    public PostController(
        IPostService postService
        // MinioClient minioClient
    )
    {
        _postService = postService;
        // _minioClient = minioClient;

        var config = new MapperConfiguration(opt => { opt.AddProfile<PostProfile>(); });

        _mapper = config.CreateMapper();
    }

    #region Create
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromForm] ReqCreatePost req)
    {
        // Todo UploadFile
        // var listKeySuccess = new List<string>();
        // foreach (var file in req.MediaFiles)
        // {
        //     var fileUploadRes = await _minioClient.UploadFile(new MinioReqUploadFile
        //     {
        //         Stream = file.OpenReadStream(), FileName = file.FileName, ContentType = file.ContentType,
        //     });
        //
        //     if (fileUploadRes is null)
        //     {
        //         foreach (var key in listKeySuccess)
        //         {
        //             await _minioClient.DeleteFile(new MinioReqDeleteFile { Key = key, });
        //         }
        //
        //         return BadRequest(new ResFailure { Message = "Upload File failure" });
        //     }
        //
        //     listKeySuccess.Add(fileUploadRes.Key);
        // }
        //
        var post = _mapper.Map<ReqCreatePost, Context.Entities.Post>(req);

        // post.MediaPath = listKeySuccess.ToArray();

        var now = DateTime.Now;
        post.CreatedDate = now;
        post.UpdatedDate = now;

        var postId = await _postService.AddAsync(post);

        return CreatedAtAction(nameof(GetOne), new { id = postId }, new ResSuccess());
    }

    #endregion

    #region Get

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ResGetPost>> GetOne([FromRoute] int id)
    {
        var post = await _postService.GetAsync(id);
        if (post is null)
            return BadRequest(new ResFailure { Message = $"Không tìm thấy bài đăng với ID: {id}" });

        return Ok(post);
    }

    #endregion
}
