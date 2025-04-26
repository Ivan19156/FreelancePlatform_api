using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Date
{
    public class FreelancePlatformDbContextFactory : IDesignTimeDbContextFactory<FreelancePlatformDbContext>
    {
        public FreelancePlatformDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Оце важливо! Щоб не було помилки з SetBasePath
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FreelancePlatformDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new FreelancePlatformDbContext(optionsBuilder.Options);
        }
    }
}
