using System;
using DomainModel.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Persistence.EF.Implementations.SQLServer.Models
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.ToTable("employee");

            builder
             .HasKey(e => new { e.id })
             .HasName("emp_pk");

            builder
             .HasIndex(e => e.name)
             .IsUnique()
             .HasDatabaseName("name_unique_idx");

            builder.Property(e => e.id)
             .HasValueGenerator<SequentialGuidValueGenerator>()
             .HasComment("GUID")
             .HasColumnName("id");

            builder
             .HasIndex(e => e.seq_id)
             .IsUnique();
             //.HasDatabaseName("seq_id_idx");

            builder
            .Property(e => e.seq_id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("seq_id")
            .HasColumnType("int");

            builder
            .Property(e => e.name)
            .ValueGeneratedNever()
            .HasComment("name is not null")
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("varchar(120)");

            builder
            .Property(e => e.salary)
            .ValueGeneratedNever()
            .HasComment("salary is not null")
            .IsRequired()
            .IsConcurrencyToken()
            .HasColumnName("salary")
            .HasColumnType("int");

            builder
            .Property(e => e.company)
            .ValueGeneratedNever()
            .HasColumnName("company")
            .HasColumnType("varchar(120)");
        }
    }
}

