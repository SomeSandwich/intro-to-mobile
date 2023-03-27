using Api.Context;
using AutoMapper;

namespace Api.Modules.Post.Services;

public interface IPostService
{
    
}

public class PostService : IPostService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;
    
    public PostService(MobileDbContext context)
    {
        _context = context;

        var config = new MapperConfiguration(opt =>
        {

        });

        _mapper = config.CreateMapper();
    }
}