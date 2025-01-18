namespace Domain.Model;

public class Meal
{
   public string MealId {get;set;}
   public string Name {get;set;}
   public List<MealItem> MealItems {get; set;} =  new List<MealItem>();
   public string EatAt {get;set;}

   // Optional: Add total calories, macros, etc. calculated from MealParts
   public double TotalCalories { get; set; }
   public double TotalProtein { get; set; }
   public double TotalCarbs { get; set; }
   public double TotalFat { get; set; }
}