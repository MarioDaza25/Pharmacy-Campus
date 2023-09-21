using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class CountryRepository : GenericRepository<Country>, ICountry
{
    private readonly PharmacyContext _context;
    public CountryRepository(PharmacyContext context) : base(context)
    {
        _context = context; 
    }
}
