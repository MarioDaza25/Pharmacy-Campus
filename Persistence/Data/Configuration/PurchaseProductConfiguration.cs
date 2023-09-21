using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class PurchaseProductConfiguration : IEntityTypeConfiguration<PurchaseProduct>
{
    public void Configure(EntityTypeBuilder<PurchaseProduct> builder)
    {
        builder.ToTable("PurchaseProduct");

        builder.HasOne(p => p.Purchase)
        .WithMany(p => p.PurchaseProducts)
        .HasForeignKey(p => p.Purchase_Fk);

        builder.HasOne(p => p.Product)
        .WithMany(p => p.PurchaseProducts)
        .HasForeignKey(p => p.Product_Fk);

        builder.Property(p => p.Quantity)
        .HasColumnType("INT")
        .IsRequired();

        builder.Property(p => p.Price)
        .HasColumnType("Decimal")
        .IsRequired();
    }
}
