using System.Xml;
using Api.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IRateService
{
    Task<bool> SyncRateAsync();
}

public class RateService : IRateService
{
    private readonly MobileDbContext _context;

    public RateService(MobileDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SyncRateAsync()
    {
        var users = await _context.Users.ToListAsync();

        foreach (var user in users)
        {
            var query = _context.Posts
                .Where(e => e.UserId == user.Id)
                .Where(e => e.IsSold == true)
                .AsQueryable();

            var rateSum = await query
                .SumAsync(e => e.Rate.Rating);

            var count = await query
                .CountAsync();

            user.Legit = (double)rateSum / count;
        }

        await _context.SaveChangesAsync();

        return true;
    }
}
