using Microsoft.AspNetCore.Mvc;
using minimal_api.Entities;
using minimal_api.Requests;
using minimal_api.Services.UserService;

namespace minimal_api.Routing
{
    public static class UserRouting
    {
        public static IEndpointRouteBuilder MapUserApi(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/user/{id}", ([FromServices] IUserService service, Guid id) => UserRequests.GetUserById(service, id))
                .WithTags("User API");

            routes.MapPost("/user/register", ([FromServices] IUserService service, [FromBody] User user) => UserRequests.Register(service, user))
                .WithTags("User API");

            return routes;
        }
    }
}