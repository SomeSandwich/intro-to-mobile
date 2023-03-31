// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Api.Context;
using Api.Context.Constants.Enums;
using Api.Context.Entities;
using Api.Modules.Auth.Types;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Modules.Auth.Services;

public interface IAccountService
{
    Task<Context.Entities.User> LoginAsync(LoginArg arg);
    Task<LoginResult> GenerateTokenAsync(Context.Entities.User user, string ipv4);
    Task<LoginResult> RefreshTokenAsync(string refreshToken, string ipv4);
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

    public async Task<Context.Entities.User> LoginAsync(LoginArg arg)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.Email == arg.Email);
        if (user == null)
        {
            throw new Exception($"Account vơ email:{arg.Email} không tìm thấy");
        }

        if (user.Status == UserStatus.Deleted)
        {
            throw new Exception("Tài khoản đã bị xóa");
        }

        if (user.Status == UserStatus.Blocked)
        {
            throw new Exception("Tài khoản bị vô hiệu hóa");
        }

        if (user.PasswordHash != arg.HashPassword)
        {
            throw new AuthenticationException("Không tài khoản hoặc mật khẩu");
        }

        return user;
    }

    public async Task<LoginResult> GenerateTokenAsync(Context.Entities.User user, string ipv4)
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
        return new LoginResult { Token = jwtToken };
    }

    public async Task<LoginResult> RefreshTokenAsync(string refreshToken, string ipv4)
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
}
