using minimal_api.Entities;
using minimal_api.Models.DTOs;

namespace minimal_api.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(User user, UserLoginDto userLoginDto);
    }
}