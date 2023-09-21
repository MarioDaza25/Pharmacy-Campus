using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class PersonTypeRepository : GenericRepository<PersonType>, IPersonType
{
    private readonly PharmacyContext _context;
    public PersonTypeRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}