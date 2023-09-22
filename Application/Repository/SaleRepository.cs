using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class SaleRepository : GenericRepository<Sale>, ISale
{
    private readonly PharmacyContext _context;
    public SaleRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> GetSaleProductCount(string product)
    {
        return await (from p in _context.Products 
                    join sp in _context.SaleProducts on p.Id equals sp.Product_Fk
                    where p.Name.ToUpper() == product.ToUpper()
                    select sp).CountAsync();
    }

}