using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class RecipeRepository : GenericRepository<Recipe>, IRecipe
{
    private readonly PharmacyContext _context;
    public RecipeRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }
}