using Api.Context.Constants.Enums;
using Api.Context.Entities;
using API.Utils;
using Newtonsoft.Json;

namespace API.Types.Objects;

public class CreateUserReq
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public string PasswordHash
    {
        get => Password.HashPassword();
    }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public double Money { get; set; }
}

public class UserAvatarReq
{
    public IFormFile File { get; set; }
}

public class UserRes
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string? AvatarPath { get; set; }

    public double Legit { get; set; }

    public string Address { get; set; }

    public double Money { get; set; }

    public UserStatus Status { get; set; }
}

public class SellerRes
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? AvatarPath { get; set; }

    public double Legit { get; set; }

    public ICollection<Post> Posts { get; set; }

    public virtual ICollection<Order> SellerOrders { get; set; }
}
