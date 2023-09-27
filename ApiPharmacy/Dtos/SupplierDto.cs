
using Domain.Entities;

namespace ApiPharmacy.Dtos;

    public class SupplierDto
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public int IdentificationType_Fk { get; set; }
        public string Name { get; set; }
        public int PersonType_Fk { get; set; }
        public int Role_Fk { get; set; }
        public int JobTitle_Fk { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public List<PurchaseDto> Purchases  { get; set; }
        
    }
public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Identification { get; set; }
    public TypeIdenDto IdentificationType { get; set; }
    public TypePDto PersonType { get; set; }
    public RoleDto Role { get; set; }
}
