using Api.Context;
using Api.Modules.User.Mapping;
using AutoMapper;

namespace Api.Modules.User.Services;

public interface IUserService
{
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
}