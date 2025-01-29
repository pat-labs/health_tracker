namespace Domain.Model;


public record struct FoodItem(
    string FoodItemId,
    string Name,
    double CaloriesPer100g,
    double ProteinPer100g,
    double CarbsPer100g,
    double FatPer100g);