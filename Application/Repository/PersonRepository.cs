using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PersonRepository : GenericRepository<Person>, IPerson
{
    private readonly PharmacyContext _context;
    public PersonRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

//      ====================================EMPLEADOS=========================================================
//=================================================================================================================

    //Obtener todos los Empleados (OK)
    public async Task<IEnumerable<Person>> GetAllEmpoyeesAsync()
    {
        return await _context.People
                .Where(p => p.Role.Description.ToUpper() == "EMPLEADO")
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
                .Include(p => p.JobTitle)
                .ToListAsync();
    }

    //Empleados que hayan hecho más de N ventas en total. (OK)
    public async Task<IEnumerable<Person>> GetSaleEmployee(int sales)
    {
        return await _context.People
                    .Where(p => p.SalesEmp.Count() > sales)
                    .Include(p => p.Role)
                    .Include(p => p.PersonType)
                    .Include(p => p.IdentificationType)
                    .Include(p => p.JobTitle)
                    .ToListAsync();
    }
    

    //Empleados que Realizaron menos de (N) Ventas el Año (N). (OK)
    public async Task<IEnumerable<Person>> GetEmployeeSaleYear(int sales, int year)
    {
        return await _context.People
                    .Where(p => p.Role.Description.ToUpper() == "EMPLEADO" &&
                     p.SalesEmp.Count(s => s.SaleDate.Year == year) < sales)
                    .Include(p => p.Role)
                    .Include(p => p.PersonType)
                    .Include(p => p.IdentificationType)
                    .Include(p => p.JobTitle)
                    .ToListAsync();
    }


    //Empleados que no realizaron ventas en (N) Mes y (N) Año. (OK)
    public async Task<IEnumerable<Person>> EmployeeNeverSaleMonthYear(int month, int year)
    {
        return await _context.People
                    .Where(p => p.Role.Description.ToUpper() == "EMPLEADO"
                    && (!p.SalesEmp.Any(s => s.SaleDate.Month == month &&  s.SaleDate.Year == year )))
                    .Include(p => p.Role)
                    .Include(p => p.PersonType)
                    .Include(p => p.IdentificationType)
                    .Include(p => p.JobTitle)
                    .ToListAsync();
    }

    //Empleados que no han realizado ninguna venta en el Año (N) (OK)
    public async Task<IEnumerable<Person>> EmployeeNeverSaleYear(int year)
    {
        return await _context.People
            .Where(p => p.Role.Description.ToUpper() == "EMPLEADO" &&
                (!p.SalesEmp.Any(s => s.SaleDate.Year == year)))
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
                .Include(p => p.JobTitle)
            .ToListAsync();
    }

    //Cantidad de ventas realizadas por cada empleado en el Año (X) (OK)
    public async Task<IEnumerable<SalesEmployeeInfo>> CountAllSalesEmployees(int year)
    {
        return  await _context.People
                .Where(p => p.Role.Description.ToUpper() == "EMPLEADO")
                .Select(employee => new SalesEmployeeInfo
                {
                    Name = employee.Name,
                    Identification = employee.Identification,
                    TotalSales = employee.SalesEmp.Count(s => s.SaleDate.Year == year)
                })
                .ToListAsync();
    }

    
//      ==================================== PACIENTES =========================================================
//=================================================================================================================

    //Obtener todos los Pacientes  (OK)
    public async Task<IEnumerable<Person>> GetAllPatientAsync()
    {
        return await _context.People
                .Where(p => p.Role.Description.ToUpper() == "PACIENTE")
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
                .ToListAsync();
    }

       
    //Pacientes que han comprado un producto especifico (OK)
    public async Task<IEnumerable<Person>> GetPurchasePatientProduct(string product)
    {
        return await _context.People
        .Where(p => p.Role.Description.ToUpper() == "PACIENTE" &&
            p.SalesPat.Any(s => s.SaleProducts.Any(sp => sp.Product.Name.ToUpper() == product.ToUpper())))
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
        .ToListAsync();
    }


    //Pacientes que compraron un producto (X) en el Año (X)  (OK)
    public async Task<IEnumerable<Person>> GetPurchasePatientProductYear(string product, int year)
    {
        return await _context.People
        .Where(p => p.Role.Description.ToUpper() == "PACIENTE" &&
            p.SalesPat.Any(s => s.SaleProducts.Any(sp => sp.Product.Name.ToUpper() == product.ToUpper() && s.SaleDate.Year == year)))
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
        .ToListAsync();
    }

    //Pacientes que no han comprado ningún medicamento en el año (X) (OK)
    public async Task<IEnumerable<Person>> GetPatientsNeverBuy(int year)
    {
        return await _context.People
            .Where(p => p.Role.Description.ToUpper() == "PACIENTE" &&
                (!p.SalesPat.Any(s => s.SaleDate.Year == year)))
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
            .ToListAsync();
    }
    
    //Total gastado por cada paciente en el Año (X) (OK)
    public async Task<IEnumerable<SpentPatient>> TotalSpentPatient(int year)
    {
        return await _context.SaleProducts
        .Where(sp => sp.Sale.SaleDate.Year == year)
        .GroupBy(sp => sp.Sale.Patient.Name) 
        .Select(group => new SpentPatient
        {
            Name = group.Key,
            TotalSpent = group.Sum(sp => sp.Price)

        })
        .ToListAsync();
    }

//      ==================================== PROVEEDORES =========================================================
//=================================================================================================================

    //Obtener todos los Proveedores (OK)
    public async Task<IEnumerable<Person>> GetAllSupplierAsync()
    {
        return await _context.People
                .Where(p => p.Role.Description.ToUpper() == "PROVEEDOR")
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
                .ToListAsync();
    }

    //Ganancia total por proveedor en el Año (X) (OK)
    public async Task<IEnumerable<SupplierGain>> TotalSupplierGain(int year)
    {
        return await _context.PurchasesProducts
        .Where(sp => sp.Purchase.PurchaseDate.Year == year)
        .GroupBy(sp => sp.Purchase.Supplier.Name) 
        .Select(group => new SupplierGain
        {
            Supplier = group.Key,
            TotalGain = Math.Round(group.Sum(sp => sp.Price * sp.Quantity))
        })
        .ToListAsync();
    }

    //Proveedores  que no han vendido ningún medicamento en Un año Especifico (OK)
    public async Task<IEnumerable<Person>> GetSupplierNeverSell(int year)
    {
        return await _context.People
            .Where(p => p.Role.Description.ToUpper() == "PROVEEDOR" &&
                (!p.Purchases.Any(s => s.PurchaseDate.Year == year)))
                .Include(p => p.Role)
                .Include(p => p.PersonType)
                .Include(p => p.IdentificationType)
            .ToListAsync();
    }



    //Total de medicamentos vendidos por cada proveedor. ?????????????????
    public async Task<IEnumerable<Person>> GetProductsSoldEachSupplierAsync() 
    {

            return await _context.People
                                .Where(p => p.Role.Description.ToUpper() == "Supplier" )
                                .Include(p => p.Role)
                                .Include(p => p.Purchases)
                                .ThenInclude(ps => ps.PurchaseProducts)
                                .ThenInclude(pp => pp.Product)
                                .ToListAsync();
        }
        //Total de medicamentos vendidos por cada proveedor.
        // public async Task<IEnumerable<Person>> GetProductsSoldEachSupplierAsync() 
        // {

        //     return await _context.People
        //                         .Include(p => p.Purchases)
        //                         .ToListAsync();                                
        // }


        //Empleado que ha vendido la mayor cantidad de medicamentos distintos en 2023.😃
        public async Task<Person> GetMajorSoldDfProductsInEmployeeAsync(int year)
        {
                return await _context.People
                    .Where(p => p.SalesEmp.Any(sp => sp.SaleDate.Year == year))
                    .Include(p => p.Role)
                    .Include(p => p.PersonType)
                    .Include(p => p.IdentificationType)
                    .Include(p => p.JobTitle)
                    .OrderByDescending(p => p.SalesEmp
                        .SelectMany(sp => sp.SaleProducts.Select(saleProduct => saleProduct.Product_Fk))
                        .Distinct()
                        .Count())
                    .FirstOrDefaultAsync();


        }
        //Paciente que ha gastado más dinero en 2023.
        public async Task<Person> GetPatientSpendMostMoneyInYear(int year)
        {
            return await _context.People
                .Where(p =>p.SalesPat.Any(sp => sp.SaleDate.Year == year))
                .OrderByDescending(s => s.SalesPat
                .SelectMany(sp => sp.SaleProducts)
                .Sum(p => p.Price))
                .FirstOrDefaultAsync();


























            //.Any(p => p.SalesPat.Sum(sp => sp.SaleProducts.))
            // .GroupBy(p => p.SalesPat.SelectMany(p => p.SaleProducts.Select(saleProduct => saleProduct.Sale_Fk)))
            // .Select(g => g.Sum(g => g.));
            //     //         .OrderByDescending( p => p.SalesPat
                // .SelectMany(sp => sp.SaleProducts.Select(saleProduct => saleProduct.Sum(p => p.price))))
                // .FirstOrDefaultAsync();
        }

}






