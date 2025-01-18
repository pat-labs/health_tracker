namespace Domain.Model;

public class MealPart 
{
   public int MealPartId { get; set; }

   public int MealId { get; set; }
   public Meal Meal { get; set; }

   public string FoodItemId { get; set; }
   public FoodItem FoodItem { get; set; }


   [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
   public double WeightInGrams { get; set; } 
   
   public double Calories => (WeightInGrams / 100) * FoodItem?.CaloriesPer100g ?? 0;
   public double Protein => (WeightInGrams / 100) * FoodItem?.ProteinPer100g ?? 0;
   public double Carbs => (WeightInGrams / 100) * FoodItem?.CarbsPer100g ?? 0;
   public double Fat => (WeightInGrams / 100) * FoodItem?.FatPer100g ?? 0;
}