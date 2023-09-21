using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPharmacy.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Identification { get; set; }
    public int IdentificationType_Fk { get; set; }
    public string Name { get; set; }
    public int PersonType_Fk { get; set; }
    public int Role_Fk { get; set; }
    public int JobTitle_Fk { get; set; }
    public DateTime HireDate { get; set; }
}
