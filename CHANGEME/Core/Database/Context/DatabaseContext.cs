using Core.Config;
using Core.Database.Entities;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Database.Context
{
    public class DatabaseContext : DbContext
    {
        private RuntimeSettings settings { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(IOptions<RuntimeSettings> appSettingsAccessor)
        {
            settings = appSettingsAccessor.Value;
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
#if DEBUG
                .UseLoggerFactory(MyLoggerFactory)
#endif
                .UseSqlServer(DatabaseConnector.GetInstance().ConnectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.FirstName).HasMaxLength(200);
                entity.Property(e => e.LastName).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(200);
            });
        }
    }
}
