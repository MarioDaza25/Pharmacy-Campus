using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiPharmacy.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, EmployeeDto>().ReverseMap();
        CreateMap<Recipe, RecipeDto>().ReverseMap();
        CreateMap<Person, SalePatientProdDto>().ReverseMap();
        CreateMap<Person, SupplierDto>().ReverseMap();
    }
}
