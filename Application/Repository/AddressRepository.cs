using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class AddressRepository : GenericRepository<Address>, IAddress
{
    private readonly PharmacyContext _context;
    public AddressRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}
