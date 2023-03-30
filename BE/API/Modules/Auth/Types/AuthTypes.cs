// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using API.Utils;

namespace Api.Modules.Auth.Types;

public class LoginReq
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }

    internal string PasswordHash
    {
        get => Password.HashPassword();
    }
}

public class LoginResult
{
    public string Token { get; set; } = string.Empty;
}

public class LoginArg
{
    public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    internal string HashPassword
    {
        get => Password.HashPassword();
    }
}
