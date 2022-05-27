using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Entities;
using minimal_api.Extensions;
using minimal_api.Models.Consts;
using minimal_api.Requests;
using minimal_api.Services.UserService;

namespace minimal_api.Routing
{
    public static class UserRouting
    {
        public static IEndpointRouteBuilder MapUserApi(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/user", ([FromServices] IUserService service) => UserRequests.GetAllUsers(service))
                .WithTags("User API");

            routes.MapGet("/user/{id}", [Authorize(ConfiguredUserPoliciesValues.AdminsOnly)] ([FromServices] IUserService service, [FromQuery] Guid id) => UserRequests.GetUserById(service, id))
                .WithTags("User API");

            routes.MapPost("/user/register", ([FromServices] IUserService service, [FromBody] User user) => UserRequests.Register(service, user))
                .WithValidator<User>()
                .WithTags("User API");

            routes.MapDelete("/user/{id}", [Authorize(ConfiguredUserPoliciesValues.AdminsOnly)] ([FromServices] IUserService service, [FromQuery] Guid id) => UserRequests.Delete(service, id))
                .WithTags("User API");
            
            routes.MapDelete("/user", [Authorize(ConfiguredUserPoliciesValues.AdminsOnly)] ([FromServices] IUserService service, [FromBody] List<Guid> ids) => UserRequests.DeleteMany(service, ids))
                .WithTags("User API");

            return routes;
        }
    }
}