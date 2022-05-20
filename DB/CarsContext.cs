using Microsoft.EntityFrameworkCore;
using minimal_api.Entities;

namespace minimal_api.DB
{
    public class CarsContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MinimalApi.sqlite;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Cars");
        }
    }
}