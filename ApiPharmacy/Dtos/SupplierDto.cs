
namespace ApiPharmacy.Dtos;

    public class SupplierDto
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public int IdentificationType_Fk { get; set; }
        public string Name { get; set; }
        public int PersonType_Fk { get; set; }
        public int Role_Fk { get; set; }
        
    }
