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

    public async Task<IEnumerable<CommentRes>> GetComment() => throw new NotImplementedException();

    public async Task<IEnumerable<CommentRes>> GetByPost(int postId) => throw new NotImplementedException();
    public async Task<bool> AddComment(CreateCommentReq request) => throw new NotImplementedException();

    public async Task<bool> RemoveComment(int id) => throw new NotImplementedException();
}
