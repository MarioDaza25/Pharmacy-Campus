using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.Property(p => p.StreetName)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(p => p.StreetNumber)
        .IsRequired()
        .HasMaxLength(25);

        builder.Property(p => p.StreetType)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(p => p.StreetTypeNumber)
        .IsRequired()
        .HasMaxLength(25);

        builder.Property(p => p.Details)
        .IsRequired()
        .HasMaxLength(300);

        builder.HasOne(p => p.Neighborhood)
        .WithMany(p => p.Addresses)
        .HasForeignKey(p => p.Neighborhood_Fk)
        .IsRequired();

        builder.HasOne(p => p.Person)
        .WithMany(p => p.Addresses)
        .HasForeignKey(p => p.Person_Fk)
        .IsRequired();
    
    
    }
}