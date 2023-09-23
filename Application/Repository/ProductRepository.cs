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
    public async Task<IEnumerable<Product>> GetAllProductExpiredBeforeAsync(DateTime ExpiryDate)
    {
        return await _context.Products
                            .Where(p => p.ExpirationDate.Date < ExpiryDate)
                            .ToListAsync();
    }
    //Medicamentos con un precio mayor a 50 y un stock menor a 100.
    public async Task<IEnumerable<Product>> GetHighPricedLowStockAsync(decimal price, double stock)
    {
        return await _context.Products
                            .Where(p => p.Price > price && p.Stock < stock)
                            .ToListAsync();
    } 

}