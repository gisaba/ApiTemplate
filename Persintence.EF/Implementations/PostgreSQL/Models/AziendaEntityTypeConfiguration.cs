using System;
using Microsoft.EntityFrameworkCore;
using DomainModel.Classes;
using DomainModel.Classes.User;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Reflection.Emit;

namespace Persistence.EF.Implementations.PostgreSQL.Models
{
	public class AziendaEntityTypeConfiguration: IEntityTypeConfiguration<Azienda>

    {
        public void Configure(EntityTypeBuilder<Azienda> builder)
        {
            builder.ToTable("company");

                    
            builder.HasKey(u => new { u.AziendaId }).HasName("company_pkey");

           // builder.HasIndex(e => e.AziendaId).IsUnique();

            builder
                .Property(u => u.AziendaId)
                .ValueGeneratedNever()
                .HasComment("id azienda")
                .IsRequired()
                .HasColumnName("id_azienda")
                .HasColumnType("bigint");

            builder
                .Property(u => u.name)
                .ValueGeneratedNever()
                .HasComment("name azienda")
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("text");
        }        
	}
}

