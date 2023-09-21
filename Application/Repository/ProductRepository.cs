using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class ProductRepository : GenericRepository<Product>, IProduct
{
    private readonly PharmacyContext _context;
    public ProductRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}