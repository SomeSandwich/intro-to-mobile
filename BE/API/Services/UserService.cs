using Api.Context;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;

namespace API.Services;

public interface IUserService
{
    Task<bool> IsUserExist(int userId);
    Task<User> AddAccountAsync(CreateUserReq arg);
    Task<IEnumerable<SellerRes>> GetMostLegit(int number);
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

    public async Task<User> AddAccountAsync(CreateUserReq arg)
    {
        var user = _mapper.Map<CreateUserReq, User>(arg);

        if (_context.Users.Any(e => e.Email == user.Email))
        {
            throw new Exception($"Email: {arg.Email}");
        }

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<IEnumerable<SellerRes>> GetMostLegit(int number)
    {
        var listSeller = _context.Users
            .OrderByDescending(e => e.Legit)
            .Take(number)
            .AsEnumerable();

        var result = _mapper.Map<IEnumerable<User>, IEnumerable<SellerRes>>(listSeller);
        return result;
    }
}
