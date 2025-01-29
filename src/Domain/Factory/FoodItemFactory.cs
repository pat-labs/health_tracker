using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Except.Driving;

namespace Domain.Factory
{
   public static class FoodItemFactory
   {
      public static IdentifierAlgorithm Snowflake { get; } = IdentifierAlgorithm.SNOWFLAKE;

      public static string IsValidFoodItemId(string foodItemId)
      {
         if (string.IsNullOrEmpty(foodItemId) || foodItemId.Length > 19)
         {
            return "Id could not be empty and the max length is 19";
         }
         return string.Empty;
      }

      public static string IsValidName(string name)
      {
         if (string.IsNullOrEmpty(name) || name.Length > 200)
         {
            return "Name could not be empty and the max length is 200";
         }
         return string.Empty;
      }

      public static string IsValidCaloriesPer100g(double caloriesPer100g)
      {
         if (caloriesPer100g < 0)
         {
            return "Calories must be positive";
         }
         return string.Empty;
      }

      public static string IsValidProteinPer100g(double proteinPer100g)
      {
         if (proteinPer100g < 0)
         {
            return "Protein must be positive";
         }
         return string.Empty;
      }

      public static string IsValidCarbsPer100g(double carbsPer100g)
      {
         if (carbsPer100g < 0)
         {
            return "Carbs must be positive";
         }
         return string.Empty;
      }

      public static string IsValidFatPer100g(double fatPer100g)
      {
         if (fatPer100g < 0)
         {
            return "Fat must be positive";
         }
         return string.Empty;
      }

      public static List<string> IsValid(FoodItem foodItem)
      {
         List<string> missingAttributes = new List<string>();
         if (string.IsNullOrEmpty(foodItem.FoodItemId))
         {
            missingAttributes.Add("foodItemId");
         }
         if (string.IsNullOrEmpty(foodItem.Name))
         {
            missingAttributes.Add("name");
         }
         if (foodItem.CaloriesPer100g <= 0)
         {
            missingAttributes.Add("caloriesPer100g");
         }
         if (foodItem.ProteinPer100g <= 0)
         {
            missingAttributes.Add("proteinPer100g");
         }
         if (foodItem.CarbsPer100g <= 0)
         {
            missingAttributes.Add("carbsPer100g");
         }
         if (foodItem.FatPer100g <= 0)
         {
            missingAttributes.Add("fatPer100g");
         }
         if (missingAttributes.Any())
         {
            return missingAttributes;
         }

         List<string> errors = new List<string>
         {
            IsValidFoodItemId(foodItem.FoodItemId),
            IsValidName(foodItem.Name),
            IsValidCaloriesPer100g(foodItem.CaloriesPer100g),
            IsValidProteinPer100g(foodItem.ProteinPer100g),
            IsValidCarbsPer100g(foodItem.CarbsPer100g),
            IsValidFatPer100g(foodItem.FatPer100g)
         };
         return errors.Where(s => !string.IsNullOrEmpty(s)).ToList();
      }

      public static List<string> IsPartialValid(FoodItem foodItem)
      {
         List<string> errors = new List<string>();
         string idError = IsValidFoodItemId(foodItem.FoodItemId);
         if (!string.IsNullOrEmpty(idError))
         {
            errors.Add(idError);
            return errors;
         }
         if (!string.IsNullOrEmpty(foodItem.Name))
         {
            errors.Add(IsValidName(foodItem.Name));
         }
         if (foodItem.CaloriesPer100g >= 0)
         {
            errors.Add(IsValidCaloriesPer100g(foodItem.CaloriesPer100g));
         }
         if (foodItem.ProteinPer100g >= 0)
         {
            errors.Add(IsValidProteinPer100g(foodItem.ProteinPer100g));
         }
         if (foodItem.CarbsPer100g >= 0)
         {
            errors.Add(IsValidCarbsPer100g(foodItem.CarbsPer100g));
         }
         if (foodItem.FatPer100g >= 0)
         {
            errors.Add(IsValidFatPer100g(foodItem.FatPer100g));
         }
         return errors.Where(e => !string.IsNullOrEmpty(e)).ToList();
      }
   }
}
