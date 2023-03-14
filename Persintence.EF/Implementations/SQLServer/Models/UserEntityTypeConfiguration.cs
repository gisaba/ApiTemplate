using System;
using Microsoft.EntityFrameworkCore;
using DomainModel.Classes.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.EF.Implementations.SQLServer.Models
{
    public class UserEntityTypeConfiguration: IEntityTypeConfiguration<UserModel>   
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");

            //builder.HasKey(c => new { c.Id, c.Username });

            builder.Property(e => e.id)
             .HasValueGenerator<SequentialGuidValueGenerator>()
             .HasComment("GUID")
             .HasColumnName("Id");

            builder.HasKey(c => new { c.username });

            builder
                .Property(u => u.username)
                .ValueGeneratedNever()
                .HasComment("Username is PK")
                .IsRequired()
                .IsConcurrencyToken()
                .HasColumnName("Username")
                .HasColumnType("varchar(50)");

            builder
                .Property(u => u.password)
                .IsRequired()
                .HasColumnName("Password");

            builder
                .Property(u => u.firstName)
                .HasColumnName("FirstName")
                .HasColumnType("varchar(50)");

            builder
                .Property(u => u.lastName)
                .HasColumnName("LastName")
                .HasColumnType("varchar(50)");

            builder.Property(u => u.timestamp)
                .HasColumnName("Timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValue("getdate()")
                .IsConcurrencyToken();

            builder.Property(u => u.timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

        }
    }
}