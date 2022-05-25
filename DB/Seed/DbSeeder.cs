using minimal_api.Entities;

namespace minimal_api.DB.Seed
{
    public class DbSeeder : IDbSeeder
    {
        private readonly MinApiContext _context;
        private readonly ILogger<DbSeeder> _logger;

        private readonly IEnumerable<Role> initRoles = new[] {
            new Role(){Name = "Admin"},
            new Role(){Name = "User"},
            new Role(){Name = "Guest"}
        };

        public DbSeeder(MinApiContext context, ILogger<DbSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Seed()
        {
            _logger.LogInformation("Try to seed");
            if (!_context.Roles.Any())
            {
                _logger.LogInformation("Start seed");
                await _context.Roles.AddRangeAsync(initRoles);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Seed completed");
            }
        }
    }
}