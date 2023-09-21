
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
            
        builder.Property(p => p.Username)
        .HasColumnName("username")
        .HasColumnType("varchar")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(p => p.Password)
        .HasColumnName("password")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Email)
        .HasColumnName("email")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();

        /* builder.HasOne(u => u.Employee)
        .WithOne(p => p.User)
        .HasForeignKey<User>(u => u.Employee_Fk); */
    }
}