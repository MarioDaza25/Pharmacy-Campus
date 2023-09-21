
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.Property(p => p.Name)
        .IsRequired()
        .HasMaxLength(150);

        builder.Property(p => p.Price)
        .IsRequired()
        .HasColumnType("Decimal");

        builder.Property(p => p.Stock)
        .IsRequired()
        .HasColumnType("Double");

        builder.Property(p => p.ExpirationDate)
        .IsRequired()
        .HasColumnType("DATETIME");

        builder.HasOne(p => p.Supplier)
        .WithMany(p => p.Products)
        .HasForeignKey(p => p.Supplier_Fk);




    }
}