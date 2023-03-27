using Api.Context;
using Api.Modules.User.Mapping;
using AutoMapper;

namespace Api.Modules.User.Services;

public interface IUserService
{
    Task<bool> IsUserExist(int userId);
}

public class UserService : IUserService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;

    public UserService(MobileDbContext context)
    {
        _context = context;

        var config = new MapperConfiguration(config => { config.AddProfile<UserProfile>(); });

        _mapper = config.CreateMapper();
    }

    public async Task<bool> IsUserExist(int userId)
    {
        return _context.Users
            .Any(e => e.Id == userId);
    }
}