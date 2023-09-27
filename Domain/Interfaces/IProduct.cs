using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product>
{
  Task<IEnumerable<Product>> GetAllInfoAsync();
  Task<IEnumerable<Product>> GetLessThanStockAsync(int amount);
  Task<IEnumerable<Product>> GetAllProductExpiredBeforeAsync(DateTime expiryDate);
  Task<IEnumerable<Product>> GetHighPricedLowStockAsync(decimal price, double stock);
  Task<IEnumerable<Product>> GetAllProductsNeverSold();
  Task<IEnumerable<Product>> GetAllProductsNotSoldInYearAsync(int year);
  Task<IEnumerable<Product>> GetAllProductsSoldInMonthAsync(int month);
  Task<IEnumerable<Product>> GetAllProductsBySupplierAsync(string supplier);
  Task<IEnumerable<TotalProductYear>> AllSalesQuarter(int year, int trim);
  Task<decimal> GetTotalGain();

}
