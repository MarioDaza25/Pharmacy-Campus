using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        
        builder.HasKey(p => new {p.User_Fk, p.Role_Fk});

        builder.HasOne(p => p.User)
        .WithMany(p => p.UsersRole)
        .HasForeignKey(p => p.User_Fk);

        builder.HasOne(p => p.JobTitle)
        .WithMany(p => p.UsersRole)
        .HasForeignKey(p => p.Role_Fk);
        
    }
}
