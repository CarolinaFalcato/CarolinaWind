using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QEnergy.Services.Persistence.EntityFramework
{
    public class QEnergyDbContextFactory : IDesignTimeDbContextFactory<QEnergyDbContext>
    {
        /// <summary>
        /// dotnet ef -v --startup-project ..\QEnergy.API migrations add {MigrationName} <-- Generate a new migration
        /// dotnet ef -v --startup-project ..\QEnergy.API migrations remove <-- Remove latest migration
        /// dotnet ef -v --startup-project ..\QEnergy.API database update <-- Update database with pending migrations
        /// </summary>
        QEnergyDbContext IDesignTimeDbContextFactory<QEnergyDbContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<QEnergyDbContext>();
            // LOCALHOST SQL SERVER
            builder.UseSqlServer("Data Source=(localdb);Initial Catalog=QEnergy;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new QEnergyDbContext(builder.Options);
        }
    }
}
