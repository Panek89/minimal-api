using Microsoft.AspNetCore.Identity;
using minimal_api.Entities;
using minimal_api.Models.Consts;

namespace minimal_api.DB.Seed
{
    public class DbSeeder : IDbSeeder
    {
        private readonly ILogger<DbSeeder> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly MinApiContext _context;

        private readonly static IEnumerable<Role> initRoles = new[] {
            new Role() { Name = UserRolesValues.Admin },
            new Role() { Name = UserRolesValues.User },
            new Role() { Name = UserRolesValues.Guest }
        };

        public DbSeeder(ILogger<DbSeeder> logger, IPasswordHasher<User> passwordHasher, MinApiContext context)
        {
            _logger = logger;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task Seed()
        {
            _logger.LogInformation("Try to seed");

            if (!_context.Cars.Any())
            {
                _logger.LogInformation("Start seed cars");
                await _context.Cars.AddRangeAsync(GenerateCars());
                await _context.SaveChangesAsync();
                _logger.LogInformation("Cars seed completed");
            }

            if (!_context.Roles.Any())
            {
                _logger.LogInformation("Start seed Roles");
                await _context.Roles.AddRangeAsync(initRoles);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Roles seed completed");
            }

            if (!_context.Users.Any())
            {
                _logger.LogInformation("Start seed Users");
                await _context.Users.AddRangeAsync(GenerateAdminUsers());
                await _context.SaveChangesAsync();
                _logger.LogInformation("Users seed completed");
            }

        }

        private IEnumerable<User> GenerateAdminUsers()
        {
            var AdminRole = _context.Roles.FirstOrDefault(r => r.Name == UserRolesValues.Admin);
            IEnumerable<User> adminUsers = new[] {
                new User() {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Login = "admin@admin.com",
                    Password = "passw",
                    Role = AdminRole
                },
                new User() {
                    FirstName = "Kam",
                    LastName = "Pan",
                    Login = "kam@pan.com",
                    Password = "passw",
                    Role = AdminRole
                }
            };
            
            foreach (var adminUser in adminUsers)
            {
                var hashedAdminUserPassword = _passwordHasher.HashPassword(adminUser, adminUser.Password);
                adminUser.Password = hashedAdminUserPassword;
            }

            return adminUsers;
        }

        private IEnumerable<Car> GenerateCars()
        {
            IEnumerable<Car> cars = new[] {
                new Car() {
                    Manufacturer = "BMW",
                    Model = "E38"
                },
                new Car() {
                    Manufacturer = "Audi",
                    Model = "A4"
                }
            };

            return cars;
        }
    }
}