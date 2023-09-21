using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("State");

        builder.Property("Name")
        .HasColumnName("StateName")
        .IsRequired()
        .HasMaxLength(50);
    
        builder.HasOne(p => p.Country)
        .WithMany(p => p.States)
        .HasForeignKey(p => p.Country_Fk);

    }
}