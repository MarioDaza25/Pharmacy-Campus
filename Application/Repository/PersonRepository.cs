using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class PersonRepository : GenericRepository<Person>, IPerson
{
    private readonly PharmacyContext _context;
    public PersonRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}