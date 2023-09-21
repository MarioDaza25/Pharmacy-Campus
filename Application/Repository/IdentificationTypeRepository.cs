using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class IdentificationTypeRepository : GenericRepository<IdentificationType>, IIdentificationType
    {
        private readonly PharmacyContext _context;
        public IdentificationTypeRepository(PharmacyContext context) : base(context)
        {
            _context = context;
        }
    }
}