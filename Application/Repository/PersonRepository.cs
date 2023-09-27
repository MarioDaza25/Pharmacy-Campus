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

    public override async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.People
                .Where(p => p.Role.Description.ToUpper() == "EMPLEADO")
                .ToListAsync();
    }

    //Pacientes que han comprado un producto especifico
    public async Task<IEnumerable<Person>> GetSalePatientProduct(string product)
    {
        return await _context.People
        .Where(p => p.Role.Description.ToUpper() == "PACIENTE" &&
            p.SalesPat.Any(s => s.SaleProducts.Any(sp => sp.Product.Name.ToUpper() == product.ToUpper())))
        .ToListAsync();
    }


    //Pacientes que compraron un "producto" en un "año" especifico
    public async Task<IEnumerable<Person>> GetSalePatientProductYear(string product, int year)
    {
        return await _context.People
        .Where(p => p.Role.Description.ToUpper() == "PACIENTE" &&
            p.SalesPat.Any(s => s.SaleProducts.Any(sp => sp.Product.Name.ToUpper() == product.ToUpper() && s.SaleDate.Year == year)))
        .ToListAsync();
    }

    //Pacientes que no han comprado ningún medicamento en Un año Especifico
    public async Task<IEnumerable<Person>> GetPatientsNeverBuy(int year)
    {
        return await _context.People
            .Where(p => p.Role.Description.ToUpper() == "PACIENTE" &&
                (!p.SalesPat.Any(s => s.SaleDate.Year == year)))
            .ToListAsync();
    }
    
    //Total gastado por cada paciente en el Año (X)
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



    //Empleados que hayan hecho más de N ventas en total.
    public async Task<IEnumerable<Person>> GetSaleEmployee(int sales)
    {
        return await _context.People
                    .Where(p => p.SalesEmp.Count() > sales)
                    .ToListAsync();
    }


    //Empleados que Realizaron menos de (N) Ventas el Año (N).
    public async Task<IEnumerable<Person>> GetEmployeeSaleYear(int sales, int year)
    {
        return await _context.People
                    .Where(p => p.Role.Description.ToUpper() == "EMPLEADO" &&
                     p.SalesEmp.Count(s => s.SaleDate.Year == year) < sales)
                    .ToListAsync();
    }

    //Empleados que no realizaron ventas en (N) Mes y (N) Año.
    public async Task<IEnumerable<Person>> EmployeeNeverSaleMonthYear(int month, int year)
    {
        return await _context.People
                    .Where(p => p.Role.Description.ToUpper() == "EMPLEADO"
                    && (!p.SalesEmp.Any(s => s.SaleDate.Month == month &&  s.SaleDate.Year == year )))
                    .ToListAsync();
    }

    //Empleados que no han realizado ninguna venta en el Año (N)
    public async Task<IEnumerable<Person>> EmployeeNeverSaleYear(int year)
    {
        return await _context.People
            .Where(p => p.Role.Description.ToUpper() == "EMPLEADO" &&
                (!p.SalesEmp.Any(s => s.SaleDate.Year == year)))
            .ToListAsync();
    }

    //Cantidad de ventas realizadas por cada empleado en Año (X)
    public async Task<IEnumerable<SalesEmployeeInfo>> CountAllSalesEmployees(int year)
    {
        return  await _context.People
                .Where(p => p.Role.Description.ToUpper() == "EMPLEADO")
                .Select(employee => new SalesEmployeeInfo
                {
                    Name = employee.Name,
                    TotalSales = employee.SalesEmp.Count(s => s.SaleDate.Year == year)
                })
                .ToListAsync();
    }

    //Ganancia total por proveedor en el Año (X)
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

    //Proveedores  que no han vendido ningún medicamento en Un año Especifico
    public async Task<IEnumerable<Person>> GetSupplierNeverSell(int year)
    {
        return await _context.People
            .Where(p => p.Role.Description.ToUpper() == "PROVEEDOR" &&
                (!p.Purchases.Any(s => s.PurchaseDate.Year == year)))
            .ToListAsync();
    }



        // //Total de medicamentos vendidos por cada proveedor.
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
        // public async Task<IEnumerable<Product>> GetMajorSoldDfProductsInEmployeeAsync()
        // {
        //     // return await _context.People
            //                     .Where(person => person.Role.Description.ToUpper() == "Employee")
            //                     .Where(person =>  
            //                     {
            //                         Person EmployeeWithMayorSoldProductDF;
            //                         int valorActual = 0;
            //                         foreach (var saleEmp in person.SalesEmp)
            //                         {
            //                             foreach (var saleProduct in saleEmp.SaleProducts)
            //                             {
            //                                 int coincidencias = 0;
            //                                 for (int i = 0; i < saleEmp.SaleProducts.Count(); i++)
            //                                 {
            //                                     if (saleProduct.Product_Fk !=  saleEmp.SaleProducts.ToList()[i].Product_Fk)
            //                                     {
            //                                         coincidencias++;
            //                                     }
            //                                 }

            //                                 if(coincidencias > valorActual)
            //                                 {
            //                                     valorActual = coincidencias;
            //                                     EmployeeWithMayorSoldProductDF = person;
            //                                 }
            //                             }
            //                         }
            //                     });
                // return _context.People
                // .Select(p => p.SalesEmp)
                // .Where(saleEmp => saleEmp.SaleDate.Year == 2023)  // Filtrar por ventas en 2023
                // .GroupBy(saleEmp => saleEmp.Employee)  // Agrupar por empleado
                // .OrderByDescending(group => group.SelectMany(saleEmp => saleEmp.SaleProducts.Select(saleProduct => saleProduct.Product_Fk)).Distinct().Count())
                // .Select(group => group.Key)
                // .FirstOrDefault();

        }





