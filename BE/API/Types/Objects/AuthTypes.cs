using System.ComponentModel.DataAnnotations;

namespace API.Types.Objects;

public class LoginReq
{
    public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}

public class LoginRes
{
    public string Token { get; set; } = string.Empty;
}
