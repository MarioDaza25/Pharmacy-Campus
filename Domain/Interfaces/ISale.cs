using Domain.Entities;

namespace Domain.Interfaces;

public interface ISale : IGenericRepository<Sale>
{
  Task<decimal> GetSaleProductCount(string product);
}
