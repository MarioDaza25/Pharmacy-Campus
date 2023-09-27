
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

    //Total de medicamentos vendidos en el trimestre (X) del Año (X). 
    public async Task<IEnumerable<TotalProductYear>> AllSalesQuarter(int year, int trim)
    {
        int month = (trim - 1) * 3 + 1;
        
        return await _context.SaleProducts
        .Where(sp => sp.Sale.SaleDate.Year == year && sp.Sale.SaleDate.Month >= month 
        && sp.Sale.SaleDate.Month <= month + 2)
        .GroupBy(sp => sp.Product.Name) 
        .Select(group => new TotalProductYear
        {
            Product = group.Key,
            Quantity = group.Sum(sp => sp.Quantity)
        })
        .ToListAsync();
    }

    //Cantidad total de dinero recaudado por las ventas de medicamentos
    public async Task<decimal> GetTotalGain()
    {
        return await _context.SaleProducts.SumAsync(sp => sp.Price);

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
    public async Task<IEnumerable<SupplierContact>> GetContactSupplierInProductAsync() 
    {
        return await _context.Products
                            .Include(p => p.Supplier)
                            .Select(p => new SupplierContact
                            {
                                Name = p.Name,
                                Price = p.Price,
                                Stock = p.Stock,
                                ExpirationDate = p.ExpirationDate,
                                SupplierName = p.Supplier.Name,
                                Email =  string.Join(", ", p.Supplier.Emails
                                    .Where(e => e.EmailType.Description.ToUpper() == "TRABAJO")
                                    .Select(e => e.Description))
                            })
                            .ToListAsync();
    }
    //Obtener el medicamento menos vendido en 2023.
    // public async Task<Product> GetLowestSellingProductAsync()
    // {
    //     return await _context.Products
    //         .OrderBy(e => e.SaleProducts.Sum(sp => sp.Quantity)) // Ordenar por la suma de las cantidades vendidas
    //         .FirstAsync(); // Tomar el primer producto (el que tiene la cantidad de ventas más baja)

    // }


}   


