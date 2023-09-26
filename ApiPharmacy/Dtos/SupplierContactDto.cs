using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiPharmacy.Dtos
{
    public class SupplierContactDto
    {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Stock { get; set; }
    public DateTime ExpirationDate  { get; set; }
    [JsonIgnore]
    public int Supplier_Fk { get; set; }
    public string SupplierName {get; set; }
    public string Email {get; set; }

    
    }
}