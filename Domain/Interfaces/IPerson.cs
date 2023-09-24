using Domain.Entities;

namespace Domain.Interfaces;

public interface IPerson : IGenericRepository<Person>
{
    Task<IEnumerable<Person>> GetSalePatientProduct(string product);
<<<<<<< HEAD
    Task<IEnumerable<Person>> GetSalePatientProductYear(string product, int date);
    Task<IEnumerable<Person>> GetSaleEmployee(int sales); 
=======
    Task<IEnumerable<Person>> GetSuppliersNoSalesAtYear();



    
>>>>>>> af3703b5ae04927605a6789fd7ea216d0b63c467
}
