using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class SaleProductRepository : GenericRepository<SaleProduct>, ISaleProduct
{
    private readonly PharmacyContext _context;
    public SaleProductRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}