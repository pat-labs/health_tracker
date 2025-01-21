namespace Domain.Model;

public class MealPart 
{
   public int MealPartId { get; set; }

// References
   public int MealId { get; set; }
   public string FoodItemId { get; set; }

// Calculated
   public double WeightInGrams { get; set; } 
   public double Calories => (WeightInGrams / 100) * FoodItem?.CaloriesPer100g ?? 0;
   public double Protein => (WeightInGrams / 100) * FoodItem?.ProteinPer100g ?? 0;
   public double Carbs => (WeightInGrams / 100) * FoodItem?.CarbsPer100g ?? 0;
   public double Fat => (WeightInGrams / 100) * FoodItem?.FatPer100g ?? 0;
}