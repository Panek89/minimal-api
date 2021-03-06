using minimal_api.Services.UserService;

namespace minimal_api.Requests
{
    public class UserRequests
    {
        public static async Task<IResult> GetAllUsers(IUserService service)
        {
            var users = await service.GetAll();

            return Results.Ok(users);
        }

        public static async Task<IResult> GetUserById(IUserService service, Guid id)
        {
            var userById = await service.GetById(id);

            return Results.Ok(userById);
        }

        public static async Task<IResult> Delete(IUserService service, Guid id)
        {
            var userToDelete = await service.GetById(id);
            if (userToDelete is null)
                return Results.NotFound();

            await service.Delete(userToDelete);

            return Results.NoContent();
        }

        public static IResult DeleteMany(IUserService service, List<Guid> ids)
        {
            service.DeleteMany(ids);
            
            return Results.NoContent();
        }
    }
}