using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class TelephoneTypeRepository : GenericRepository<TelephoneType>, ITelephoneType
{
    private readonly PharmacyContext _context;
    public TelephoneTypeRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

}
