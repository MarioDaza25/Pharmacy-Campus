
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
{
    public void Configure(EntityTypeBuilder<SaleProduct> builder)
    {
        builder.ToTable("SaleProduct");

        builder.HasOne(p => p.Sale)
        .WithMany(p => p.SaleProducts)
        .HasForeignKey(p => p.Sale_Fk);

        builder.HasOne(p => p.Product)
        .WithMany(p => p.SaleProducts)
        .HasForeignKey(p => p.Product_Fk);

        builder.Property(p => p.Quantity)
        .HasColumnType("INT")
        .IsRequired();

        builder.Property(p => p.Price)
        .HasColumnType("Decimal")
        .IsRequired();



    }
}