using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiPharmacy.Profiles;

public class MappingProfiles : Profile
{
    protected MappingProfiles()
    {
        CreateMap<Person, EmployeeDto>().ReverseMap();
    }
}
