using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace minimal_api.Routing
{
    public static class AuthRouting
    {
        public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder routes, IConfiguration configuration)
        {
            routes.MapGet("/token", () =>
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "user-id"),
                    new Claim(ClaimTypes.Name, "Test Name"),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var token = new JwtSecurityToken
                (
                    issuer: configuration["JwtValidIssuer"],
                    audience: configuration["JwtValidIssuer"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(60),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSigningKey"])),
                        SecurityAlgorithms.HmacSha256)
                );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                
                return jwtToken;
            });

            routes.MapGet("/user", (ClaimsPrincipal user) =>
            {
                var userName = user.Identity.Name;
                return $"Hello {userName}";
            });

            return routes;
        }
    }
}