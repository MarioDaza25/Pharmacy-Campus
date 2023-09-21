
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class TelephoneRepository : GenericRepository<Telephone>, ITelephone
{
    private readonly PharmacyContext _context;
    public TelephoneRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}