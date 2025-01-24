using Domain.Model;

namespace Service.DrivingAdapter.RestAdapter.Dto.Mapping;


public static class FoodItemMapper
{
    public static FoodItem AdaptToModel(FoodItemDto foodItemDto)
    {
        return new FoodItem
        {
            FoodItemId = foodItemDto.FoodItemId,
            Name = foodItemDto.Name,
            CaloriesPer100g = foodItemDto.CaloriesPer100g,
            ProteinPer100g = foodItemDto.ProteinPer100g,
            CarbsPer100g = foodItemDto.CarbsPer100g,
            FatPer100g = foodItemDto.FatPer100g
        };
    }

    public static FoodItemDto AdaptToDto(FoodItem foodItem)
    {
        return new FoodItemDto
        {
            FoodItemId = foodItem.FoodItemId,
            Name = foodItem.Name,
            CaloriesPer100g = foodItem.CaloriesPer100g,
            ProteinPer100g = foodItem.ProteinPer100g,
            CarbsPer100g = foodItem.CarbsPer100g,
            FatPer100g = foodItem.FatPer100g
        };
    }
}