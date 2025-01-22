using AutoMapper;
using Domain.Model;

namespace Service.DrivingAdapter.RestAdapter.Dto.Mapping;

public class FoodItemMappingProfile : Profile
{
   public FoodItemMappingProfile()
   {
      CreateMap<FoodItem, FoodItemDto>();
   }
}