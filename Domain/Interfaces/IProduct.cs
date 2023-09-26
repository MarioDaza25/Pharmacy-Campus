using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product>
{   
  Task<IEnumerable<TotalProductYear>> AllSalesQuarter(int year, int trim);
  Task<decimal> GetTotalGain();
}
