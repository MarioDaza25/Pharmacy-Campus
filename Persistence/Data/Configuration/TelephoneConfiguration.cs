
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class TelephoneConfiguration : IEntityTypeConfiguration<Telephone>
{
    public void Configure(EntityTypeBuilder<Telephone> builder)
    {
        builder.ToTable("Telephone");

        builder.Property(p => p.PhoneNumber)
        .IsRequired()
        .HasMaxLength(15);

        builder.HasOne(p => p.Person)
        .WithMany(p => p.Telephones)
        .HasForeignKey(p => p.Person_Fk);

        builder.HasOne(p => p.TelephoneType)
        .WithMany(p => p.Telephones)
        .HasForeignKey(p => p.TelephoneType_Fk);



    
    }
}