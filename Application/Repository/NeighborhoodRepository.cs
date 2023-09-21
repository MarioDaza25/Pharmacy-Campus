using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class NeighborhoodRepository : GenericRepository<Neighborhood>, INeighborhood
{
    private readonly PharmacyContext _context;
    public NeighborhoodRepository(PharmacyContext context) : base(context)
    {
        _context = context; 
    } 
}