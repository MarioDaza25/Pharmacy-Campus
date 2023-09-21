using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class SaleRepository : GenericRepository<Sale>, ISale
{
    private readonly PharmacyContext _context;
    public SaleRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}