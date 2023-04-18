using Api.Context;
using Api.Context.Entities;
using API.Types.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ICommentService
{
    Task<CommentRes> GetComment(int id);
    Task<IEnumerable<CommentRes>> GetComment();
    Task<IEnumerable<CommentRes>> GetByPost(int postId);

    Task<bool> AddComment(CreateCommentReq request);

    Task<bool> RemoveComment(int id);
}

public class CommentService : ICommentService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;

    public CommentService(MobileDbContext context, IMapper mapper)
    {
        _context = context;

        _mapper = mapper;
    }

    public async Task<CommentRes> GetComment(int id)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(e => e.Id == id);

        return _mapper.Map<Comment, CommentRes>(comment);
    }

    public async Task<IEnumerable<CommentRes>> GetComment()
    {
        var cmts = await _context.Comments
            .ToListAsync();

        return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentRes>>(cmts);
    }

    public async Task<IEnumerable<CommentRes>> GetByPost(int postId)
    {
        var cmts = await _context.Comments
            .Where(e => e.PostId == postId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentRes>>(cmts);
    }

    public async Task<bool> AddComment(CreateCommentReq request)
    {
        var cmt = _mapper.Map<CreateCommentReq, Comment>(request);

        await _context.Comments.AddAsync(cmt);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveComment(int id)
    {
        var cmt = await _context.Comments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (cmt is null)
            return false;

        _context.Comments.Remove(cmt);

        await _context.SaveChangesAsync();

        return true;
    }
}
