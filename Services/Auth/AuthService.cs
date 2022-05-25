using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using minimal_api.DB;

namespace minimal_api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly MinApiContext _context;

        public AuthService(IConfiguration config, MinApiContext context)
        {
            _config = config;
            _context = context;
        }

        public string GenerateToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user-id"),
                new Claim(ClaimTypes.Name, "Test Name"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var token = new JwtSecurityToken
            (
                issuer: _config["JwtValidIssuer"],
                audience: _config["JwtValidIssuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSigningKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwtToken;
        }
    }
}