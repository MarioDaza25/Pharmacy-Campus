using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class PurchaseRepository : GenericRepository<Purchase>, IPurchase
{
    private readonly PharmacyContext _context;
    public PurchaseRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}