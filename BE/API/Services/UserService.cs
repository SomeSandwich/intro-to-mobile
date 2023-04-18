using Api.Context;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IUserService
{
    Task<bool> IsUserExist(int userId);
    Task<UserRes> Get(int id);

    Task<User> AddAccountAsync(CreateUserReq arg);
    Task<IEnumerable<SellerRes>> GetMostLegit(int number);

    Task<bool> UpdateAvatarAsync(int id, string avatar);
}

public class UserService : IUserService
{
    private readonly MobileDbContext _context;
    private readonly IMapper _mapper;

    public UserService(MobileDbContext context, IMapper mapper)
    {
        _context = context;

        _mapper = mapper;
    }

    public async Task<bool> IsUserExist(int userId)
    {
        return _context.Users
            .Any(e => e.Id == userId);
    }

    public async Task<UserRes> Get(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.Id == id);

        return _mapper.Map<User, UserRes>(user);
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

    public async Task<bool> UpdateAvatarAsync(int id, string avatar)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.Id == id);

        if (user is null)
            return false;

        user.AvatarPath = avatar;

        await _context.SaveChangesAsync();

        return true;
    }
}
