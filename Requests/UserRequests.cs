using minimal_api.Entities;
using minimal_api.Services.UserService;

namespace minimal_api.Requests
{
    public class UserRequests
    {
        public static IResult GetUserById(IUserService service, Guid id)
        {
            var userById = service.GetById(id);

            return Results.Ok(userById);
        }

        public static async Task<IResult> Register(IUserService service, User user)
        {
            await service.Register(user);

            return Results.Created("", user);
        }
    }
}