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
}
