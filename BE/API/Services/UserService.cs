using Api.Context;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Services;

public interface IUserService
{
    Task<bool> IsUserExist(int userId);

<<<<<<< HEAD
    Task<IEnumerable<SellerRes>> GetMostLegit(int number);

=======
    Task<User> AddAccountAsync(CreateUserReq arg);
>>>>>>> 3910f3b50c084e1f07714a5924340b4dbc205949
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

<<<<<<< HEAD
    public async Task<IEnumerable<SellerRes>> GetMostLegit(int number)
    {
        var listSeller = _context.Users
            .OrderByDescending(e => e.Legit)
            .Take(number)
            .AsEnumerable();

        var result =  _mapper.Map<IEnumerable<User>, IEnumerable<SellerRes>>(listSeller);
        return result;
=======
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
>>>>>>> 3910f3b50c084e1f07714a5924340b4dbc205949
    }
}
