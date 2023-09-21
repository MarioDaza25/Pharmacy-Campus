using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RecipeProductRepository : GenericRepository<RecipeProduct>, IRecipeProduct
{
    private readonly PharmacyContext _context;
    public RecipeProductRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}