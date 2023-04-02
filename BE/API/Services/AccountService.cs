using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Api.Context;
using Api.Context.Constants.Enums;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using API.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public interface IAccountService
{
    Task<User> LoginAsync(LoginReq req);
    Task<LoginRes> GenerateTokenAsync(User user, string ipv4);
    Task<LoginRes> RefreshTokenAsync(string refreshToken, string ipv4);
    Task<User> AddAccountAsync(CreateUserReq arg);
}

public class AccountService : IAccountService
{
    private readonly MobileDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AccountService(MobileDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

        var mapperConfig = new MapperConfiguration(opt => { opt.AddProfile<AuthProfile>(); });
        _mapper = mapperConfig.CreateMapper();
    }

    public async Task<User> LoginAsync(LoginReq req)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.Email == req.Email);
        if (user == null)
        {
            throw new Exception($"Not found User with Email:{req.Email}");
        }

        if (user.Status == UserStatus.Deleted)
        {
            throw new Exception("User was deleted!!!");
        }

        if (user.Status == UserStatus.Blocked)
        {
            throw new Exception("User was disabled");
        }

        if (!req.Password.ValidatePassword(user.PasswordHash))
        {
            throw new AuthenticationException("Wrong Password");
        }

        return user;
    }

    public async Task<LoginRes> GenerateTokenAsync(User user, string ipv4)
    {
        var tokenExpires = DateTime.UtcNow.AddHours(7).AddMinutes(30);
        var secretKey = _configuration["JWT:Secret"]!;

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(secretKey);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        };
        var token = new JwtSecurityToken
        (expires: tokenExpires,
            claims: authClaims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature));

        var jwtToken = jwtTokenHandler.WriteToken(token);
        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            AddedDate = DateTime.UtcNow.AddHours(7),
            ExpiryDate = DateTime.UtcNow.AddHours(31),
            IsRevoked = false,
            Token = jwtToken,
            IpAddress = ipv4,
            UserId = user.Id
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        return new LoginRes { Token = jwtToken };
    }

    public async Task<LoginRes> RefreshTokenAsync(string refreshToken, string ipv4)
    {
        var oldToken = await _context.RefreshTokens
            .Where(e => e.Token == refreshToken)
            .FirstOrDefaultAsync();

        if (oldToken is null) throw new AuthenticationException("Wrong token");

        var account = await _context.Users
            .Where(e =>
                e.Id == oldToken.UserId &&
                e.Status != UserStatus.Deleted)
            .FirstOrDefaultAsync();

        if (account is null)
        {
            throw new AuthenticationException("Account is deleted or not exists");
        }

        oldToken.IsUsed = true;
        _context.RefreshTokens.Update(oldToken);
        await _context.SaveChangesAsync();

        return await GenerateTokenAsync(account, ipv4);
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
}
