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

        CreateMap<Person, SalePatientProdDto>().ReverseMap()
            .ForMember(dest => dest.IdentificationType_Fk, opt => opt.Ignore())
            .ForMember(dest => dest.PersonType_Fk, opt => opt.Ignore())
            .ForMember(dest => dest.Role_Fk, opt => opt.Ignore())
            .ForMember(dest => dest.JobTitle_Fk, opt => opt.Ignore());
    }
}
