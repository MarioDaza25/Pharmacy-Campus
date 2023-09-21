
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");

        builder.Property(p => p.Identification)
        .IsRequired()
        .HasMaxLength(20);

        builder.Property(p => p.Name)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.HireDate)
        .HasColumnType("DATETIME");

        builder.HasOne(p => p.JobTitle)
        .WithMany(p => p.People)
        .HasForeignKey(p => p.JobTitle_Fk);

        builder.HasOne(p => p.IdentificationType)
        .WithMany(p => p.People)
        .HasForeignKey(p => p.IdentificationType_Fk)
        .IsRequired();

        builder.HasOne(p => p.Role)
        .WithMany(p => p.People)
        .HasForeignKey(p => p.Role_Fk)
        .IsRequired();

        builder.HasOne(p => p.PersonType)
        .WithMany(p => p.People)
        .HasForeignKey(p => p.PersonType_Fk)
        .IsRequired();
        
    }
}
