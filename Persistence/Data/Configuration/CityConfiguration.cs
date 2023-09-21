
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City");

        builder.Property("Name")
        .HasColumnName("CityName")
        .IsRequired()
        .HasMaxLength(50);

        builder.HasOne(p => p.State)
        .WithMany(p => p.Cities)
        .HasForeignKey(p => p.State_Fk);
    
    }
}