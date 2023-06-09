﻿using System.Runtime.InteropServices;
using System.Security.Claims;
using Api.Context.Constants.Enums;
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
    [Route("")]
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

        if (orderId is null)
            return StatusCode(500, new FailureRes());

        return Ok(new SuccessRes());
    }

    [HttpGet]
    [Route("{orderId:int}")]
    public async Task<ActionResult<CreateOrderReq>> GetOne([FromRoute] int orderId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString is null)
        {
            return Unauthorized();
        }

        int.TryParse(userIdString, out int userId);

        try
        {
            var order = await _orderService.GetAsync(userId, orderId);

            if (order is null)
                return BadRequest(new FailureRes { Message = $"Không tồn tại orderId: {orderId}" });

            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return Forbid(ex.Message);
        }
    }

    [HttpGet]
    [Route("purchase")]
    public async Task<ActionResult<IEnumerable<CreateOrderReq>>> GetPurchase()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString is null)
        {
            return Unauthorized();
        }

        int.TryParse(userIdString, out int userId);

        var listPurchase = await _orderService.GetByCustomerId(userId);

        return Ok(listPurchase);
    }

    [HttpGet]
    [Route("postId/{postId:int}")]
    public async Task<ActionResult> GetOrderByPostIdAsync([FromRoute] int postId)
    {
        var order = await _orderService.GetByPostId(postId);

        return Ok(order);
    }


    [HttpGet]
    [Route("sale")]
    public async Task<ActionResult<IEnumerable<CreateOrderReq>>> GetSale()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString is null)
        {
            return Unauthorized();
        }

        int.TryParse(userIdString, out int userId);

        var listSale = await _orderService.GetBySellerId(userId);

        return Ok(listSale);
    }

    [HttpPatch]
    [Route("{orderId:int}/address")]
    public async Task<ActionResult> UpdateAddress([FromRoute] int orderId, [FromBody] UpdateOrderAddressReq req)
    {
        var customerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (customerIdString is null)
        {
            return Unauthorized();
        }

        int.TryParse(customerIdString, out int customerId);

        req.CustomerId = customerId;

        var update = await _orderService.UpdateAddressAsync(orderId, req);

        if (update == false)
            return BadRequest(
                new FailureRes { Message = $"Thay đổi địa chỉ giao hàng cho orderId: {orderId} thất bại" });
        return Ok(new SuccessRes());
    }

    [HttpPatch]
    [Route("{orderId:int}/status")]
    public async Task<ActionResult> UpdateStatus([FromRoute] int orderId, [FromBody] OrderStatus status)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdString is null)
        {
            return Unauthorized();
        }

        int.TryParse(userIdString, out int userId);

        return Ok(new SuccessRes());
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
