using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class StateRepository : GenericRepository<State>, IState
{
    private readonly PharmacyContext _context;
    public StateRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}