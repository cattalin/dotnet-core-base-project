using Infrastructure.Config;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Core.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
#if DEBUG
                .UseLoggerFactory(MyLoggerFactory)
#endif
                .UseSqlServer(AppConfig.ConnectionStrings.AzureDatabase, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Fields
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.FirstName).HasMaxLength(200);
                entity.Property(e => e.LastName).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.PasswordHash).HasMaxLength(500);
                entity.Property(e => e.PasswordSalt).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.FirstName).HasMaxLength(200);
                entity.Property(e => e.LastName).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(200);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
            });
            #endregion

            #region Relationships
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithOne(e => e.Account)
                    .HasForeignKey<User>(e => e.Id)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Company)
                    .WithMany(e => e.Accounts)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasMany(e => e.Departments)
                    .WithOne(e => e.Company)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.Users)
                    .WithOne(e => e.Company)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion
        }
    }
}
