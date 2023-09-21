
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.ToTable("Email");

        builder.Property(p => p.Description)
        .IsRequired()
        .HasMaxLength(255);

        builder.HasOne(p => p.Person)
        .WithMany(p => p.Emails)
        .HasForeignKey(p => p.Person_Fk);

        builder.HasOne(p => p.EmailType)
        .WithMany(p => p.Emails)
        .HasForeignKey(p => p.EmailType_Fk);



    
    }
}