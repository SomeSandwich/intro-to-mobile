using Api.Context;
using Api.Modules.Post.Mapping;
using Api.Modules.Post.Type;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Post.Services;

public interface IPostService
{
    Task<int> AddAsync(Context.Entities.Post post);

    Task<ResGetPost> GetAsync(int id);
    Task<IEnumerable<ResGetPost>> GetByShopAsync(int shopId);

    Task<bool> UpdateAsync(int id, ArgUpdatePost args);

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

    public async Task<int> AddAsync(Context.Entities.Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return post.Id;
    }

    #endregion

    #region Get

    public async Task<ResGetPost> GetAsync(int id)
    {
        var post = await _context.Posts
            .Include(e => e.Reports)
            .Where(e => (e.IsDeleted != true && e.IsHide != true))
            .FirstOrDefaultAsync(e => e.Id == id);

        if (post is null)
            return null;

        return _mapper.Map<Context.Entities.Post, ResGetPost>(post);
    }

    public async Task<IEnumerable<ResGetPost>> GetByShopAsync(int shopId)
    {
        var listPost = _context.Posts
            .Where(e => e.IsDeleted == false)
            .AsEnumerable();


        return _mapper.Map<IEnumerable<Context.Entities.Post>, IEnumerable<ResGetPost>>(listPost);
    }

    #endregion

    #region Update

    public async Task<bool> UpdateAsync(int id, ArgUpdatePost args)
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

    #endregion

    #region Delete

    public async Task<bool> DeleteAsync(int id)
    {
        var post = _context.Posts.FirstOrDefault(e => e.Id == id);

        if (post is null)
            return false;

        post.IsDeleted = true;
        post.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        return true;
    }

    #endregion
}
