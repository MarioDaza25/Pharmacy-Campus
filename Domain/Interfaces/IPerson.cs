using Domain.Entities;

namespace Domain.Interfaces;

public interface IPerson : IGenericRepository<Person>
{
    Task<IEnumerable<Person>> GetSalePatientProduct(string product);
    Task<IEnumerable<Person>> GetSuppliersNoSalesAtYear();



    
}
