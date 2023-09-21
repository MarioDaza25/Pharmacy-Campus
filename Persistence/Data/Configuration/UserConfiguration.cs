
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

        builder.HasOne(u => u.Employee)
        .WithMany(p => p.Users)
        .HasForeignKey(u => u.Employee_Fk);

        builder.HasMany(p => p.JobsTitle)
        .WithMany(p => p.Users)
        .UsingEntity<UserRole>(
            p => p
                .HasOne(p => p.JobTitle)
                .WithMany(p => p.UsersRole)
                .HasForeignKey(p => p.Role_Fk),
            p => p
                .HasOne(p => p.User)
                .WithMany(p => p.UsersRole)
                .HasForeignKey(p => p.User_Fk),
            p =>
            {
                p.HasKey(p => new {p.Role_Fk, p.User_Fk});
            }
        );
    }
}