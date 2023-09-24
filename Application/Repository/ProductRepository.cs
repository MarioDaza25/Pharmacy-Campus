using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class ProductRepository : GenericRepository<Product>, IProduct
{
    private readonly PharmacyContext _context;
    public ProductRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetLessThanStockAsync(int amount)
    {
        return await _context.Products
                            .Where(p => p.Stock < amount)
                            .ToListAsync();
    }

 // creacion Repositiorio de Medicamentos que caducan antes del 1 de enero de 2024.
    public async Task<IEnumerable<Product>> GetAllProductExpiredBeforeAsync(DateTime expiryDate)
    {
        return await _context.Products
                            .Where(p => p.ExpirationDate.Date < expiryDate)
                            .ToListAsync();
    }
    //Medicamentos con un precio mayor a 50 y un stock menor a 100.
    public async Task<IEnumerable<Product>> GetHighPricedLowStockAsync(decimal price, double stock)
    {
        return await _context.Products
                            .Where(p => p.Price > price && p.Stock < stock)
                            .ToListAsync();
    } 
    //Medicamentos que no han sido vendidos nunca.
    public async Task<IEnumerable<Product>> GetAllProductsNeverSold()
    {
        return await _context.Products
                            .Where(p => !p.SaleProducts.Any())
                            .ToListAsync();
    }
    //Medicamentos que no han sido vendidos en 2023.
    public async Task<IEnumerable<Product>> GetAllProductsNotSoldInYearAsync(DateTime year)
    {

        return await _context.Products
                            .Where(product =>
                                !product.SaleProducts.Any(saleProduct =>
                                    saleProduct.Sale.SaleDate >= year.AddYears(-1) && saleProduct.Sale.SaleDate <= year.Date))
                                    .ToListAsync();
    }                      
    public async Task<IEnumerable<Product>> GetAllProductsSoldInMonthAsync(int month)
    {
        return await _context.Products
                            .Where(product =>
                                product.SaleProducts.Any(saleProduct =>
                                    saleProduct.Sale.SaleDate.Month == month))
                                    .ToListAsync();
    }
    //Medicamentos comprados al ‘Proveedor A’.
    public async Task<IEnumerable<Product>> GetAllProductsBySupplierAsync(string supplier)
    {
        return await _context.Products
                            .Where(product =>
                                product.PurchaseProducts.Any(purchaseProduct =>
                                    purchaseProduct.Purchase.Supplier.Name == supplier ))
                                    .ToListAsync();
                            
    } 
}
