using System.Reactive;
using Api.Context;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IPostService
{
    Task<int> AddAsync(Api.Context.Entities.Post post);

    Task<GetPostRes?> GetAsync(int id);
    Task<IEnumerable<GetPostRes>> GetByShopIdAsync(int shopId);
    Task<IEnumerable<GetPostRes>> GetLatestAsync(int number);

    Task<bool> UpdateAsync(int id, UpdatePostArgs args);
    Task<bool> ToggleIsHide(int id);

    Task<bool> DeleteAsync(int id);
}

public class PostService : IPostService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;

    public PostService(MobileDbContext context)
    {
        _context = context;

        var config = new MapperConfiguration(opt => { opt.AddProfile<PostProfile>(); });

        _mapper = config.CreateMapper();
    }

    #region Create

    public async Task<int> AddAsync(Api.Context.Entities.Post post)
    {
        try
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region Get

    public async Task<GetPostRes?> GetAsync(int id)
    {
        var post = await _context.Posts
            .Include(e => e.Reports)
            .Where(e => (e.IsDeleted != true && e.IsHide != true))
            .FirstOrDefaultAsync(e => e.Id == id);

        if (post is null)
            return null;

        return _mapper.Map<Api.Context.Entities.Post, GetPostRes>(post);
    }

    public async Task<IEnumerable<GetPostRes>> GetByShopIdAsync(int shopId)
    {
        var listPost = _context.Posts
            .Where(e => e.IsDeleted == false)
            .AsEnumerable();


        return _mapper.Map<IEnumerable<Api.Context.Entities.Post>, IEnumerable<GetPostRes>>(listPost);
    }

    public async Task<IEnumerable<GetPostRes>> GetLatestAsync(int number)
    {
        var listPost = _context.Posts
            .Where(e => e.IsDeleted == false && e.IsHide == false)
            .OrderByDescending(e => e.CreatedDate)
            .Take(number)
            .AsEnumerable();


        return _mapper.Map<IEnumerable<Api.Context.Entities.Post>, IEnumerable<GetPostRes>>(listPost);
    }

    #endregion

    #region Update

    public async Task<bool> UpdateAsync(int id, UpdatePostArgs args)
    {
        var post = _context.Posts
            .Where((e => e.IsDeleted == false))
            .FirstOrDefault(e => e.Id == id);

        if (post is null)
            return false;


        var lsit = post.MediaPath.ToList();

        foreach (var file in args.MediaFilesDelete)
        {
            lsit.Remove(file);
        }

        foreach (var file in args.MediaFilesAdd)
        {
            lsit.Add(file);
        }

        post.MediaPath = lsit.ToArray();

        if (args.Price is not null)
            post.Price = (int)args.Price;

        if (!string.IsNullOrEmpty(args.Caption))
            post.Caption = args.Caption;

        if (!string.IsNullOrEmpty(args.Description))
            post.Description = args.Description;

        post.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ToggleIsHide(int id)
    {
        var post = await _context.Posts
            .FirstOrDefaultAsync(e => e.Id == id);

        if (post is null)
            return false;

        post.IsHide = !post.IsHide;

        await _context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Delete

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(e => e.Id == id);

        if (post is null)
            return false;

        post.IsDeleted = true;
        post.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return true;
    }

    #endregion
}
