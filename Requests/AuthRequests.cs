using minimal_api.Models.DTOs;
using minimal_api.Services.Auth;
using minimal_api.Services.UserService;

namespace minimal_api.Requests
{
    public class AuthRequests
    {
        public static async Task<IResult> GenerateToken(IAuthService service, IUserService userService, UserLoginDto userLoginDto)
        {
            var userToFind = await userService.GetByLogin(userLoginDto.Login);
            if (userToFind is null)
                return Results.NotFound();

            var token = service.GenerateToken(userToFind, userLoginDto);

            return Results.Ok(token);
        }
    }
}