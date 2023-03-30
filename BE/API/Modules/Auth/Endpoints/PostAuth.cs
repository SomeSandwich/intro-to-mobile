using Api.Modules.Auth.Services;
using Api.Modules.Auth.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Auth.Endpoints;

public static class PostAuth
{
    public static IEndpointRouteBuilder PostLoginEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router)
    {
        endpoints.MapPost(router, async (
            [FromServices] IAccountService accountService,
            HttpRequest httpRequest,
            [FromBody] LoginArg arg) =>
        {
            var account = await accountService.LoginAsync(arg);
            var remoteIpAddress = httpRequest.HttpContext.Connection.RemoteIpAddress;
            var ipv4 = "";
            if (remoteIpAddress != null) ipv4 = remoteIpAddress.MapToIPv4().ToString();

            var result = await accountService.GenerateTokenAsync(account, ipv4);
            return Results.Ok(result);
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder RefreshTokenEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router)
    {
        endpoints.MapPost(router, async (
            [FromServices] IAccountService accountService,
            IConfiguration configuration,
            HttpRequest request,
            string tokenExpried) =>
        {
            try
            {
                var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress;
                var ipv4 = "";
                if (remoteIpAddress != null) ipv4 = remoteIpAddress.MapToIPv4().ToString();
                var result = await accountService.RefreshTokenAsync(tokenExpried, ipv4);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        });

        return endpoints;
    }
}
