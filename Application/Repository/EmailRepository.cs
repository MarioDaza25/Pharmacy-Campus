using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class EmailRepository : GenericRepository<Email>, IEmail
{
    private readonly PharmacyContext _context;
    public EmailRepository(PharmacyContext context) : base(context)
    {
        _context = context; 
    }
}
