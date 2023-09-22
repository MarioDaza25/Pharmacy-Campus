using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetLessThanStockAsync(int amount);
    Task<IEnumerable<Product>> GetAllProductExpiredBeforeAsync(DateTime ExpiryDate);
    Task<IEnumerable<Product>> GetHighPricedLowStockAsync(decimal price, double stock);
}
