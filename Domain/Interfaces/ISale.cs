using Domain.Entities;

namespace Domain.Interfaces;

public interface ISale : IGenericRepository<Sale>
{
  Task<int> GetSaleProductCount(string product);
}
