
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipe");

        builder.Property(p => p.CreateDate)
        .IsRequired();

        builder.HasOne(p => p.Doctor)
        .WithMany(p => p.RecipesDoc)
        .HasForeignKey(p => p.Doctor_Fk)
        .IsRequired();

        builder.HasOne(p => p.Patient)
        .WithMany(p => p.RecipesPat)
        .HasForeignKey(p => p.Patient_Fk)
        .IsRequired();

    
    }
}