
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchase");

        builder.Property(p => p.PurchaseDate)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.HasOne(p => p.Supplier)
        .WithMany(p => p.Purchases)
        .HasForeignKey(p => p.Supplier_Fk)
        .IsRequired();
    }
}