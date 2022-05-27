using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Models.DTOs;
using minimal_api.Requests;
using minimal_api.Services.Auth;
using minimal_api.Services.UserService;

namespace minimal_api.Routing
{
    public static class AuthRouting
    {
        public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder routes, IConfiguration configuration)
        {
            routes.MapPost("/auth/token", ([FromServices] IAuthService service, [FromServices] IUserService userService, [FromBody] UserLoginDto userLoginDto) => 
                        AuthRequests.GenerateToken(service, userService, userLoginDto))
                .Produces<string>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Auth API");

            return routes;
        }
    }
}
