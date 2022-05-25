using minimal_api.Services.Auth;

namespace minimal_api.Requests
{
    public class AuthRequests
    {
        public static IResult GenerateToken(IAuthService service)
        {
            var token = service.GenerateToken();

            return Results.Ok(token);
        }
    }
}