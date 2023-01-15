using Microsoft.EntityFrameworkCore;
using QEnergy.Core.Domain.Entities;

namespace QEnergy.Services.Persistence.EntityFramework
{
    public class QEnergyDbContext : DbContext
    {
        public QEnergyDbContext(DbContextOptions<QEnergyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCompanies(modelBuilder);
            ConfigureUsers(modelBuilder);
            ConfigureProjects(modelBuilder);
            //ConfigureWindTurbineGenerators(modelBuilder);
        }

        private static void ConfigureCompanies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(company =>
            {
                company.ToTable("Companies").HasKey(c => c.Id);

                company.Property(c => c.Name).IsRequired().HasMaxLength(255);

                company.HasIndex(c => c.Id).IsUnique();
            });
        }

        private static void ConfigureUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users");

                user.Property(u => u.Id).ValueGeneratedOnAdd();
                user.Property(u => u.Username).IsRequired().HasMaxLength(255);
                user.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                user.Property(u => u.Name).HasMaxLength(255);
                user.Property(u => u.Surname).HasMaxLength(255);
                user.Property(u => u.Email).HasMaxLength(255);

                user.HasIndex(u => u.Id).IsUnique();
                user.HasIndex(u => u.Username).IsUnique();
                user.HasIndex(u => u.Email).IsUnique();
            });

        }

        private static void ConfigureProjects(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(project =>
            {
                project.ToTable("Projects");

                project.Property(p => p.Id).ValueGeneratedOnAdd();
                project.Property(p => p.Name).IsRequired().HasMaxLength(255);
                project.Property(p => p.ProjectNumber).IsRequired().HasMaxLength(255);
                project.Property(p => p.Number3lCode).IsRequired().HasMaxLength(3);
                project.Property(p => p.DealType).IsRequired();
                project.Property(p => p.Group).IsRequired().HasMaxLength(255);
                project.Property(p => p.Status).IsRequired();
                project.Property(p => p.WTGNumbers);
                project.Property(p => p.Kw);
                project.Property(p => p.MonthsAquired);

                project.Property(p => p.Created)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                project.Property(p => p.Modified)
                    .ValueGeneratedOnUpdate()
                    .HasDefaultValueSql("SYSUTCDATETIME()");


                project.HasIndex(p => p.Id).IsUnique();
                project.HasIndex(p => p.ProjectNumber).IsUnique();
                project.HasIndex(p => p.Number3lCode).IsUnique();
                project.HasIndex(p => p.DealType);
                project.HasIndex(p => p.Group);
                project.HasIndex(p => p.Status);
            });

        }

        private static void ConfigureWindTurbineGenerators(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WindTurbineGenerator>(entity =>
            {
                entity.ToTable("WindTurbineGenerators");

                entity.HasIndex(x => x.Id).IsUnique();
            });

        }
    }
}
