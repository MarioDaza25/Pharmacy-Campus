
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class RecipeProductConfiguration : IEntityTypeConfiguration<RecipeProduct>
{
    public void Configure(EntityTypeBuilder<RecipeProduct> builder)
    {
        builder.ToTable("RecipeProduct");

        builder.HasOne(p => p.Product)
        .WithMany(p => p.RecipeProducts)
        .HasForeignKey(p => p.Product_Fk);

        builder.Property(p => p.Quantity)
        .HasColumnType("INT")
        .IsRequired();

        builder.HasOne(p => p.Recipe)
        .WithMany(p => p.RecipeProducts)
        .HasForeignKey(p => p.Recipe_Fk);
    }
}

