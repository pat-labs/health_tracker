#nullable disable warnings

namespace Service.DrivingAdapter.RestAdapter.Dto;


public record struct FoodItemDto(string FoodItemId, string Name, double CaloriesPer100g, double ProteinPer100g, double CarbsPer100g, double FatPer100g);