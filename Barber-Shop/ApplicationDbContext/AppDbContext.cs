using Barber_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Barber_Shop.ApplicationDbContext
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("SQLServerConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Scheduling> Scheduling { get; set; }
    }
}
