using AutoMapper;
using WalkAPI.Models.Domain;
using WalkAPI.Models.DTO;

namespace WalkAPI.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Region, RegionsDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();

            CreateMap<Walk, WalksDto>().ReverseMap();
            CreateMap<AddWalksRequestDto,Walk>().ReverseMap();
        }
    }
}
