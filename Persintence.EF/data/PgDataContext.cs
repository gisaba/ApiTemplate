using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using DomainModel.CQRS.Queries.GetUsers;
using DomainModel.Classes.User;
using DomainModel.Classes;
using Microsoft.Extensions.Configuration;
using System.Runtime.ConstrainedExecution;
using Microsoft.Extensions.Options;
using Npgsql;
using Persistence.EF.Implementations.PostgreSQL.Models;

namespace Persistence.EF.data
{
    public class PgDataContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Azienda> Aziendas { get; set; }

        public IConfiguration Configuration { get; }

        private readonly DbContextOptions<PgDataContext> _dbContextOptions;

        public PgDataContext(DbContextOptions<PgDataContext> options, IConfiguration configuration)
            : base(options)
        {
            _dbContextOptions = options;
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Postgres"),
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),null);
                }
             );
        }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("api");

            //// Manual configuration
            //modelBuilder.Entity<Azienda>()
            //    .HasMany(b => b.Employees)
            //    .WithOne(a => a.Azienda);

            //modelBuilder.Entity<Azienda>()
            //    .Navigation(b => b.Employees)
            //    .UsePropertyAccessMode(PropertyAccessMode.Property);

            // Manual configuration
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Azienda)
                .WithMany(e => e.Employees);

            modelBuilder.Entity<Employee>()
                .Navigation(b => b.Azienda)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            /**********************************/

            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<UserModel>());

            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());

            new AziendaEntityTypeConfiguration().Configure(modelBuilder.Entity<Azienda>());
        }
        #endregion
    }
}

