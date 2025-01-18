using AutoMapper;
using Domain.Model;

namespace Service.DrivingAdapter.RestAdapter.Dto.Mapping;

public class FoodItemMappingProfile : FoodItemMappingProfile
{
   public FoodItemMappingProfile()
   {
      CreateMap<FoodItem, FoodItemDto>(); 
   }
}