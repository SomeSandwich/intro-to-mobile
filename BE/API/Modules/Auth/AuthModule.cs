// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using API.App.Extensions;
using Api.Modules.Auth.Endpoints;
using Api.Modules.Auth.Services;
using Api.Modules.Post.Endpoints;
using Asp.Versioning.Builder;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Modules.Auth;

public class AuthModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        builder.AddScoped<IAccountService, AccountService>();
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var authGroup = endpoints.MapGroup($"{GlobalConfig.BaseRoute}/auth")
            .WithDisplayName("Authentication")
            .WithTags("Authentication")
            .WithApiVersionSet(version)
            .HasApiVersion(1);

        authGroup.PostLoginEndpoint("/login");
        authGroup.RefreshTokenEndpoint("/refresh-token");


        return endpoints;
    }
}
