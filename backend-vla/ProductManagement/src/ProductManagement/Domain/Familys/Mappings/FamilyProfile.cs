namespace ProductManagement.Domain.Familys.Mappings;

using ProductManagement.Dtos.Family;
using AutoMapper;
using ProductManagement.Domain.Familys;

public class FamilyProfile : Profile
{
    public FamilyProfile()
    {
        //createmap<to this, from this>
        CreateMap<Family, FamilyDto>()
            .ReverseMap();
        CreateMap<FamilyForCreationDto, Family>();
        CreateMap<FamilyForUpdateDto, Family>()
            .ReverseMap();
    }
}