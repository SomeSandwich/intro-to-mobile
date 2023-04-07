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
    public readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult> AddPostToCartAsync([FromRoute] int id, [FromBody] int postId)
    {
        var cart = new Cart { UserId = id, PostId = postId };

        var add = await _cartService.Create(cart);

        if (add == false)
            return BadRequest(new FailureRes
            {
                Message = $"Thêm PostId: {postId} vào giỏ hàng của UserId: {id} thất bại !!!"
            });

        return CreatedAtAction(nameof(GetByUserId), new { userId = id }, new SuccessRes());
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IEnumerable<Cart>> GetByUserId([FromRoute] int userId)
    {
        var listPost = await _cartService.GetByUserId(userId);

        return listPost;
    }

    [HttpDelete]
    [Route("{userId}/post/{postId}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int userId, [FromRoute] int postId)
    {
        var delete = await _cartService.Delete(userId, postId);

        if (delete == false)
            return BadRequest(new FailureRes { Message = $"Xoá PostId: {postId} từ UserID: {userId} thất bại !!!" });

        return Ok(new SuccessRes());
    }

    [HttpDelete]
    [Route("userId")]
    public async Task<ActionResult> DeleteByUserIdAsync([FromRoute] int userId)
    {
        await _cartService.DeleteByUserId(userId);

        return Ok(new SuccessRes());
    }

    [HttpDelete]
    [Route("{postId}")]
    public async Task<ActionResult> DeleteByPostIdAsync([FromRoute] int postId)
    {
        await _cartService.DeleteByPostId(postId);

        return Ok(new SuccessRes());
    }
}
