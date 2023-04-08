using Api.Context;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IPostService
{
    Task<int> AddAsync(Post post);

    Task<PostRes?> GetAsync(int id);
    Task<IEnumerable<PostRes>> GetByShopIdAsync(int shopId);
    Task<IEnumerable<PostRes>> GetLatestAsync(int number);

    Task<bool> UpdateAsync(int id, UpdatePostArgs args);
    Task<bool> ToggleIsHide(int id);

    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<PostRes>> GetByCategoryAsync(int categoryId, int number = 10, string sort = "inc");
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

    public async Task<int> AddAsync(Post post)
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

    public async Task<PostRes?> GetAsync(int id)
    {
        var post = await _context.Posts
            .Include(e => e.Reports)
            .Where(e => (e.IsDeleted != true && e.IsHide != true))
            .FirstOrDefaultAsync(e => e.Id == id);

        if (post is null)
            return null;

        return _mapper.Map<Post, PostRes>(post);
    }

    public async Task<IEnumerable<PostRes>> GetByShopIdAsync(int shopId)
    {
        var listPost = _context.Posts
            .Where(e => e.IsDeleted == false)
            .AsEnumerable();


        return _mapper.Map<IEnumerable<Post>, IEnumerable<PostRes>>(listPost);
    }

    public async Task<IEnumerable<PostRes>> GetLatestAsync(int number)
    {
        var listPost = _context.Posts
            .Where(e => e.IsDeleted == false && e.IsHide == false)
            .OrderByDescending(e => e.CreatedDate)
            .Take(number)
            .AsEnumerable();


        return _mapper.Map<IEnumerable<Post>, IEnumerable<PostRes>>(listPost);
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


        var list = post.MediaPath.ToList();

        foreach (var file in args.MediaFilesDelete)
        {
            list.Remove(file);
        }

        list.AddRange(args.MediaFilesAdd);

        post.MediaPath = list.ToArray();

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

    public async Task<IEnumerable<PostRes>> GetByCategoryAsync(int categoryId, int number, string sort)
    {
        var query = _context.Posts
            // #TODO: Generated Post and Enable this comment
            // .Where(e => e.IsHide != false)
            // .Where(e => e.IsDeleted != false)
            .Where(e => e.CategoryId == categoryId)
            .AsQueryable();

        if (sort == "desc")
        {
            query = query
                .OrderByDescending(e => e.CreatedDate)
                .AsQueryable();
        }
        else
        {
            query = query
                .OrderBy(e => e.CreatedDate)
                .AsQueryable();
        }

        return query
            .Take(number)
            .Select(e => _mapper.Map<Post, PostRes>(e))
            .AsEnumerable();
    }

    #endregion
}