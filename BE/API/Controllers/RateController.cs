using API.Services;
using API.Types.Objects;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{v:apiVersion}/rate")]
public class RateController : ControllerBase
{
    private readonly IRateService _rateService;


    public RateController(IRateService rateService)
    {
        _rateService = rateService;
    }

    [HttpGet]
    [Route("sync")]
    public async Task<IActionResult> SyncRate()
    {
        if (!await _rateService.SyncRateAsync())
            return StatusCode(500, new FailureRes { Message = "Sync Rate thất bại" });

        return Ok();
    }
}
