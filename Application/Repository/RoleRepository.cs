using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RoleRepository : GenericRepository<Role>, IRole
{
    private readonly PharmacyContext _context;
    public RoleRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}