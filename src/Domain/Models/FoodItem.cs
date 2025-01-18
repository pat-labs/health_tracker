namespace Domain.Model;

public class FoodItem
{
   public string FoodItemId { get; set; }
   [Required]
   public string Name { get; set; }
   public double CaloriesPer100g { get; set; }
   public double ProteinPer100g { get; set; }
   public double CarbsPer100g { get; set; }
   public double FatPer100g { get; set; }
}