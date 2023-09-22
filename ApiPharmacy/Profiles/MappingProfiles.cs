using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiPharmacy.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, EmployeeDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();

    }
}
