using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Requests;
using minimal_api.Services.Auth;

namespace minimal_api.Routing
{
    public static class AuthRouting
    {
        public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder routes, IConfiguration configuration)
        {
            routes.MapGet("/auth/token", ([FromServices] IAuthService service) => AuthRequests.GenerateToken(service))
                .WithTags("Auth API");

            return routes;
        }
    }
}
