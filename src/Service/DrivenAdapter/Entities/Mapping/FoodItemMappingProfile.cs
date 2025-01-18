using AutoMapper;
using Domain.Model;

namespace Service.DrivenAdapter.DatabaseAdapter.Entities.Mappings;

public class FoodItemMappingProfile : Profile
{
    public FoodItemMappingProfile()
    {
        CreateMap<FoodItemEntity, FoodItem>();
    }
}