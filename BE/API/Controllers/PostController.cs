using Api.Context.Entities;
using API.Services;
using API.Types.Mapping;
using API.Types.Objects;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/post")]
public class PostController : ControllerBase
{
    private readonly IPostService _postSer;

    private readonly IMapper _mapper;

    public PostController(IPostService postSer)
    {
        _postSer = postSer;

        var config = new MapperConfiguration(opt => { opt.AddProfile<PostProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAll()
    {
        return Ok();
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

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> Create([FromForm] CreatePostReq createPostReq)
    {
        // Todo: UploadFile
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
        var post = _mapper.Map<CreatePostReq, Post>(createPostReq);

        // post.MediaPath = listKeySuccess.ToArray();

        var now = DateTime.Now;
        post.CreatedDate = now;
        post.UpdatedDate = now;

        var postId = await _postSer.AddAsync(post);

        return CreatedAtAction(nameof(GetId), new { id = postId }, new ResSuccess());
    }
}
