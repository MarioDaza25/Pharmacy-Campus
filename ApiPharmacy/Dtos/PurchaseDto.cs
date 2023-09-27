using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiPharmacy.Dtos
{
    public class PurchaseDto
    {
        //public DateTime PurchaseDate { get; set; }
        //public int Supplier_Fk { get; set; }
        public List<ProductDto> PurchaseProducts { get; set; }
    }
}