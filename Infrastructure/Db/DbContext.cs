using Microsoft.EntityFrameworkCore;
using API.NET.Entitys;

namespace API.NET.Infrastructure.Db
{
    public class DataBase : DbContext
    {
        private readonly IConfiguration _configurationAppSettings;
        
        public DataBase(IConfiguration configurationAppSettings)
        {
            _configurationAppSettings = configurationAppSettings;
        }

        public DbSet<Administrator> Administrators { get; set; } = default!;
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasData
            (
                new Administrator
                {
                    Id = 1,
                    Email = "adm@teste.com",
                    Password = "123456",
                    Profile = "Admin"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conectionString = _configurationAppSettings.GetConnectionString("mysql")?.ToString();
                if (!string.IsNullOrEmpty(conectionString))
                {
                    optionsBuilder.UseMySql(conectionString,
                    ServerVersion.AutoDetect(conectionString));
                }
            }
        }
    }
}