using Domain.Entities;

namespace Domain.Interfaces;

public interface IPerson : IGenericRepository<Person>
{
    Task<IEnumerable<Person>> GetSalePatientProduct(string product);

    Task<IEnumerable<Person>> GetSalePatientProductYear(string product, int date);
    Task<IEnumerable<Person>> GetPatientsNeverBuy(int date);
    Task<IEnumerable<SpentPatient>> TotalSpentPatient(int year);
    Task<IEnumerable<Person>> GetSaleEmployee(int sales); 
    Task<IEnumerable<Person>> EmployeeNeverSaleMonthYear(int month, int year);
    Task<IEnumerable<Person>> EmployeeNeverSaleYear(int year);
    Task<IEnumerable<Person>> GetEmployeeSaleYear(int sales, int year);
    Task<IEnumerable<SalesEmployeeInfo>> CountAllSalesEmployees(int year);
    Task<IEnumerable<SupplierGain>> TotalSupplierGain(int year);
    Task<IEnumerable<Person>> GetSupplierNeverSell(int year);

}
