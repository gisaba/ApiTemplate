using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using DomainModel.CQRS.Queries.GetUsers;
using DomainModel.Classes.User;
using DomainModel.Classes;
using Microsoft.Extensions.Configuration;
using System.Runtime.ConstrainedExecution;
using Microsoft.Extensions.Options;
using Persistence.EF.Implementations.SQLServer.Models;

namespace Persistence.EF.data
{
    public class DataContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public IConfiguration Configuration { get; }

        private readonly DbContextOptions<DataContext> _dbContextOptions;


        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
            : base(options)
        {
            _dbContextOptions = options;
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlServer"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    }
                 );
        }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<UserModel>());

            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());
        }
        #endregion
    }
}

