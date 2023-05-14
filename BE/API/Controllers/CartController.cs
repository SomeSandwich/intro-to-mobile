using System.Security.Claims;
using Api.Context.Entities;
using API.Services;
using API.Types.Objects;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:ApiVersion}/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetByUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(userIdString, out int userId);

        var listPost = await _cartService.GetByUserId(userId);

        return Ok(listPost);
    }

    [HttpPost]
    [Route("{postId:int}")]
    public async Task<ActionResult> AddPostToCartAsync([FromRoute] int postId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(userIdString, out int id);

        var cart = new Cart { UserId = id, PostId = postId };

        var add = await _cartService.Create(cart);

        if (add == false)
            return BadRequest(new FailureRes
            {
                Message = $"Thêm PostId: {postId} vào giỏ hàng của UserId: {id} thất bại !!!"
            });

        return CreatedAtAction(nameof(GetByUserId), new { userId = id }, new SuccessRes());
    }

    [HttpDelete]
    [Route("{postId:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int postId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString is null)
            return Unauthorized(new FailureRes { Message = "Not login!" });
        int.TryParse(userIdString, out int userId);

        var delete = await _cartService.Delete(userId, postId);

        if (delete == false)
            return BadRequest(new FailureRes { Message = $"Xoá PostId: {postId} từ UserID: {userId} thất bại !!!" });

        return Ok(new SuccessRes());
    }

    // [HttpDelete]
    // [Route("userId")]
    // public async Task<ActionResult> DeleteByUserIdAsync([FromRoute] int userId)
    // {
    //     await _cartService.DeleteByUserId(userId);
    //
    //     return Ok(new SuccessRes());
    // }
    //
    // [HttpDelete]
    // [Route("{postId}")]
    // public async Task<ActionResult> DeleteByPostIdAsync([FromRoute] int postId)
    // {
    //     await _cartService.DeleteByPostId(postId);
    //
    //     return Ok(new SuccessRes());
    // }
}
