using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class JobTitleRepository : GenericRepository<JobTitle>, IJobTitle
{
    private readonly PharmacyContext _context;
    public JobTitleRepository(PharmacyContext context) : base(context)
    {
        _context = context; 
    } 
}