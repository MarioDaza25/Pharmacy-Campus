using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product>
<<<<<<< HEAD
{
    Task<IEnumerable<Product>> GetLessThanStockAsync(int amount);
    Task<IEnumerable<Product>> GetAllProductExpiredBeforeAsync(DateTime ExpiryDate);
    Task<IEnumerable<Product>> GetHighPricedLowStockAsync(decimal price, double stock);
=======
{   
>>>>>>> f4a3fadce8a7051146e98d06efe41a55bc61f8ff
}
