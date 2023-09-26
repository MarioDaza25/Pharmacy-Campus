using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiPharmacy.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, EmployeeDto>().ReverseMap();
        CreateMap<Product,ProductDto>().ReverseMap();
        CreateMap<Recipe, RecipeDto>().ReverseMap();
        CreateMap<Person, SalePatientProdDto>().ReverseMap();
        CreateMap<Person, SupplierDto>().ReverseMap();
        CreateMap<SalesEmployeeInfo, SalesEmployeDto>().ReverseMap();
        CreateMap<TotalProductYear, TotalProductDto>().ReverseMap();
        CreateMap<SpentPatient, SpentPatientDto>().ReverseMap(); 
        CreateMap<SupplierGain, SupplierGainDto>().ReverseMap();
        CreateMap<SupplierContact, SupplierContactDto>().ReverseMap();
    }
}
