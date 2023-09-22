using Domain.Entities;

namespace Domain.Interfaces;

public interface IRecipe : IGenericRepository<Recipe>
{
    Task<IEnumerable<Recipe>> GetRecordsByDate(DateTime date);
}
