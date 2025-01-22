using AutoMapper;
using Domain.Model;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entities.Mapping;

public class FoodItemMappingProfile : Profile
{
   public FoodItemMappingProfile()
   {
      CreateMap<FoodItemEntity, FoodItem>();
   }
}