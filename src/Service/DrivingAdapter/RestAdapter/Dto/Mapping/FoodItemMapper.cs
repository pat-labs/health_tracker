using AutoMapper;

using Domain.Model;

namespace Service.DrivingAdapter.RestAdapter.Dto.Mapping;


public class PandaMappingProfile : Profile
{
    public PandaMappingProfile()
    {
        CreateMap<FoodItem, FoodItemDto>();
        CreateMap<FoodItemDto, FoodItem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}