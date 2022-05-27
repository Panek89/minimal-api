using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                .WithTags(ApiNamesValues.UserApi);

            routes.MapGet("/user/{id}", [Authorize(ConfiguredUserPoliciesValues.AdminsOnly)] ([FromServices] IUserService service, [FromQuery] Guid id) => UserRequests.GetUserById(service, id))
                .WithTags(ApiNamesValues.UserApi);

            routes.MapDelete("/user/{id}", [Authorize(ConfiguredUserPoliciesValues.AdminsOnly)] ([FromServices] IUserService service, [FromQuery] Guid id) => UserRequests.Delete(service, id))
                .WithTags(ApiNamesValues.UserApi);
            
            routes.MapDelete("/user", [Authorize(ConfiguredUserPoliciesValues.AdminsOnly)] ([FromServices] IUserService service, [FromBody] List<Guid> ids) => UserRequests.DeleteMany(service, ids))
                .WithTags(ApiNamesValues.UserApi);

            return routes;
        }
    }
}