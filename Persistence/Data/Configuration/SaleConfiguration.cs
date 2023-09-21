
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.Property(p => p.SaleDate)
        .HasColumnType("DateTime")
        .IsRequired();

        builder.HasOne(p => p.Patient)
        .WithMany(p => p.SalesPat)
        .HasForeignKey(p => p.Patient_Fk)
        .IsRequired();

        builder.HasOne(p => p.Employee)
        .WithMany(p => p.SalesEmp)
        .HasForeignKey(p => p.Employee_Fk)
        .IsRequired();

        builder.HasOne(p => p.PaymentMethod)
        .WithMany(p => p.Sales)
        .HasForeignKey(p => p.PaymentMethod_Fk)
        .IsRequired();
        
    
    }
}