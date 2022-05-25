using minimal_api.Entities;

namespace minimal_api.DB.Seed
{
    public class DbSeeder : IDbSeeder
    {
        private readonly MinApiContext _context;
        private readonly ILogger<DbSeeder> _logger;

        private readonly static IEnumerable<Role> initRoles = new[] {
            new Role() { Name = "Admin" },
            new Role() { Name = "User" },
            new Role() { Name = "Guest" }
        };

        public DbSeeder(MinApiContext context, ILogger<DbSeeder> logger)
        {
            _context = context;
            _logger = logger;
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
            var AdminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
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