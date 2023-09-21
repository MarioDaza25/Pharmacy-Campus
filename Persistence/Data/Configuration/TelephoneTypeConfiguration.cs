
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class TelephoneTypeConfiguration : IEntityTypeConfiguration<TelephoneType>
{
    public void Configure(EntityTypeBuilder<TelephoneType> builder)
    {
        builder.ToTable("TelephoneType");

        builder.Property(p => p.Description)
        .IsRequired()
        .HasMaxLength(45);

    
    }
}