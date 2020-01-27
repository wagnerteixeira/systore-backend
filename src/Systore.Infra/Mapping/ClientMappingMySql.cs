using Systore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Systore.Infra.Mapping
{
    public class ClientMappingMySql : IEntityTypeConfiguration<Client>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("client");

            builder.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(150);
            //builder.Property(p => p.Code);
            builder.Property(p => p.RegistryDate)
                .HasColumnType("DATE")
               .HasConversion(
                    c => TimeZoneHelper.ConvertTimeFromUtc(c),
                    c => TimeZoneHelper.ConvertTimeToUtc(c)
                );


            builder.Property(p => p.DateOfBirth)
                .HasColumnType("DATE")
                .HasConversion(
                    c => TimeZoneHelper.ConvertTimeFromUtc(c),
                    c => TimeZoneHelper.ConvertTimeToUtc(c)
                );

            builder.Property(p => p.Address)
                .HasMaxLength(150);

            builder.Property(p => p.Neighborhood)
                .HasMaxLength(50);

            builder.Property(p => p.City)
                .HasMaxLength(50);

            builder.Property(p => p.State)
                .HasMaxLength(50);

            builder.Property(p => p.PostalCode)
                .HasMaxLength(20);

            builder.Property(p => p.Cpf)
                .HasMaxLength(20);

            builder.Property(p => p.Seller)
                .HasMaxLength(30);

            builder.Property(p => p.JobName)
                .HasMaxLength(60);

            builder.Property(p => p.Occupation)
                .HasMaxLength(50);

            builder.Property(p => p.PlaceOfBirth)
                .HasMaxLength(50);

            builder.Property(p => p.Spouse)
                .HasMaxLength(150);

            builder.Property(p => p.Note);

            builder.Property(p => p.Phone1)
                .HasMaxLength(20);

            builder.Property(p => p.Phone2)
                .HasMaxLength(20);

            builder.Property(p => p.AddressNumber)
                .HasMaxLength(20);

            builder.Property(p => p.Rg)
                .HasMaxLength(20);

            builder.Property(p => p.Complement)
                .HasMaxLength(50);

            builder.Property(p => p.AdmissionDate)
                .HasColumnType("DATE")
                .HasConversion(
                    c => TimeZoneHelper.ConvertTimeFromUtc(c),
                    c => TimeZoneHelper.ConvertTimeToUtc(c)
                );

            builder.Property(p => p.CivilStatus)
                .HasColumnType("TINYINT");

            builder.Property(p => p.FatherName)
                .HasMaxLength(150);

            builder.Property(p => p.MotherName)
                .HasMaxLength(150);

        }
    }
}