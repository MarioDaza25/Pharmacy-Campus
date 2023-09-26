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

    
}