
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class TelephoneConfiguration : IEntityTypeConfiguration<Telephone>
{
    public void Configure(EntityTypeBuilder<Telephone> builder)
    {
        builder.ToTable("Telephone");

    
    }
}