using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QEnergy.Core.Domain.Configuration;
using QEnergy.Services.Persistence.EntityFramework;

namespace QEnergy.API.Data
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<QEnergyDbContext>
    {
        public QEnergyDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<QEnergyDbContext>();
            var databaseConfiguration = new DatabaseConfiguration();
            configuration.GetSection(nameof(DatabaseConfiguration)).Bind(databaseConfiguration);
            var connectionString = string.Format(databaseConfiguration.ConnectionString, databaseConfiguration.User, databaseConfiguration.Password);
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("QEnergy.Services.Persistence.EntityFramework"));

            return new QEnergyDbContext(builder.Options);
        }

    }
}
