using API.Services;
using API.Types.Mapping;
using API.Types.Objects;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _catSer;

    private readonly IMapper _mapper;

    public CategoryController(ICategoryService catSer)
    {
        _catSer = catSer;

        var config = new MapperConfiguration(opt => { opt.AddProfile<CategoryProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<CategoryRes>> GetAll()
    {
        var categories = await _catSer.Get();

        return Ok(categories);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<CategoryRes>> GetId([FromRoute] int id)
    {
        var categories = await _catSer.Get(id);

        return categories is null ? BadRequest() : Ok(categories);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> Create([FromBody] CreateCategoryReq request)
    {
        if (!await _catSer.Create(request))
        {
            return BadRequest();
        }

        return Ok(new ResSuccess());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryReq request)
    {
        if (!await _catSer.Update(id, request))
        {
            return BadRequest();
        }

        return Ok(new ResSuccess());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (!await _catSer.Delete(id))
        {
            return BadRequest();
        }

        return Ok(new ResSuccess());
    }
}
