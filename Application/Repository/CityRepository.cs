using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class CityRepository : GenericRepository<City>, ICity
{
    private readonly PharmacyContext _context;
    public CityRepository(PharmacyContext context) : base(context)
    {
        _context = context; 
    }
}