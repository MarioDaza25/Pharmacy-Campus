using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class EmailTypeRepository : GenericRepository<EmailType>, IEmailType
{
    private readonly PharmacyContext _context;
    public EmailTypeRepository(PharmacyContext context) : base(context)
    {
        _context = context; 
    }      
}
