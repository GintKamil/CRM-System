using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CRMSystem.Infrastructure.DB
{
    public class DataBaseContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();

            optionsBuilder.UseNpgsql(
                "User ID=asptest;Password=1234;Host=localhost;Port=5432;Database=CRMSystem;Pooling=true");

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
