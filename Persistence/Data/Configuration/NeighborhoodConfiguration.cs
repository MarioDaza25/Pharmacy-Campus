
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.ToTable("Neighborhood");
                
        builder.Property("Name")
        .HasColumnName("NeighborhoodName")
        .IsRequired()
        .HasMaxLength(50);
               
        builder.HasOne(p => p.City)
        .WithMany(p => p.Neighborhoods)
        .HasForeignKey(p => p.City_Fk);
    
    }
}