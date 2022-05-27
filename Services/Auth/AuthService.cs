using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using minimal_api.DB;
using minimal_api.Entities;
using minimal_api.Models.Consts;
using minimal_api.Models.DTOs;

namespace minimal_api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly MinApiContext _context;

        public AuthService(IConfiguration config, IPasswordHasher<User> passwordHasher, MinApiContext context)
        {
            _config = config;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task Register(User user)
        {
            if (user.Role == null) 
            {
                user.Role = _context.Roles.FirstOrDefault(x => x.Name == UserRolesValues.Guest);
            }

            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
            user.Password = hashedPassword;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public string GenerateToken(User user, UserLoginDto userLoginDto)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userLoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadHttpRequestException("Invalid Username or Password");
            }
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Login),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.Name),
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