using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiPharmacy.Dtos;
    public class ProductsSoldEachSupplierDto
    {
        public string SupplierName {get;set;}
        public List<PurchaseProduct> PurchaseProducts {get;set;} 

    }