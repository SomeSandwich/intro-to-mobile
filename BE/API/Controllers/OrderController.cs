using System.Security.Claims;
using Api.Context.Constants.Enums;
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
[Route("api/v{v:ApiVersion}/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;

        var config = new MapperConfiguration(opt => { opt.AddProfile<OrderProfile>(); });
        _mapper = config.CreateMapper();
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(CreateOrderReq request)
    {
        var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (customerId is null)
        {
            return Unauthorized();
        }

        int.TryParse(customerId, out int id);

        request.CustomerId = id;

        var orderId = await _orderService.AddAsync(request);

        return CreatedAtAction(nameof(GetOne), new { id = orderId }, new ResSuccess());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<CreateOrderReq>> GetOne([FromBody] int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("purchase")]
    public async Task<ActionResult<IEnumerable<CreateOrderReq>>> GetPurchase()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("sale")]
    public async Task<ActionResult<IEnumerable<CreateOrderReq>>> GetSale()
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    [Route("{id:int}/address")]
    public async Task<ActionResult> UpdateAddress([FromRoute] int id, [FromBody] UpdateOrderAddressReq req)
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    [Route("{id:int}/status")]
    public async Task<ActionResult> UpdateStatus([FromRoute] int id, [FromBody] OrderStatus status)
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    [Route("{id:int}/total")]
    public async Task<ActionResult> UpdateTotal([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
