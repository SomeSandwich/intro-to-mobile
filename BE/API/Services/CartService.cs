using Api.Context;
using Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ICartService
{
    Task<bool> Create(Cart request);

    Task<IEnumerable<Cart>> GetByUserId(int userId);

    Task<bool> Delete(int userId, int postId);
    Task DeleteByUserId(int userId);
    Task DeleteByPostId(int postId);

    Task<Cart> Get(int userId, int postId);
}

public class CartService : ICartService
{
    private readonly MobileDbContext _context;

    public CartService(MobileDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(Cart request)
    {
        var cartPost = await Get(request.UserId, request.PostId);

        if (cartPost is not null)
            return false;

        _context.Carts.Add(request);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Cart>> GetByUserId(int userId)
    {
        var listPost = _context.Carts
            .Where(e => e.UserId == userId)
            .Include(e => e.Post)
            .AsEnumerable();

        return listPost;
    }

    public async Task<bool> Delete(int userId, int postId)
    {
        var cartPost = await Get(userId, postId);

        if (cartPost is null)
            return false;

        _context.Carts.Remove(cartPost);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task DeleteByUserId(int userId)
    {
        var carts = _context.Carts
            .Where(e => e.UserId == userId)
            .AsEnumerable();

        _context.Carts.RemoveRange(carts);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByPostId(int postId)
    {
        var carts = _context.Carts
            .Where(e => e.PostId == postId)
            .AsEnumerable();

        _context.Carts.RemoveRange(carts);
        await _context.SaveChangesAsync();
    }

    public async Task<Cart?> Get(int userId, int postId)
    {
        var cartPost = _context.Carts.FirstOrDefault(e =>  (e.PostId == postId && e.UserId == userId ));

        return cartPost;
    }
}
