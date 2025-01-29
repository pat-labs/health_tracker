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
         return "";
      }

      public static string IsValidName(string name)
      {
         if (string.IsNullOrEmpty(name) || name.Length > 200)
         {
            return "Name could not be empty and the max length is 200";
         }
         return "";
      }

      public static string IsValidCaloriesPer100g(double caloriesPer100g)
      {
         if (caloriesPer100g < 0)
         {
            return "Calories must be positive";
         }
         return "";
      }

      public static string IsValidProteinPer100g(double proteinPer100g)
      {
         if (proteinPer100g < 0)
         {
            return "Protein must be positive";
         }
         return "";
      }

      public static string IsValidCarbsPer100g(double carbsPer100g)
      {
         if (carbsPer100g < 0)
         {
            return "Carbs must be positive";
         }
         return "";
      }

      public static string IsValidFatPer100g(double fatPer100g)
      {
         if (fatPer100g < 0)
         {
            return "Fat must be positive";
         }
         return "";
      }

      public static List<string> MissingValues(string foodItemId, string name, double caloriesPer100g, double proteinPer100g, double carbsPer100g, double fatPer100g)
      {
         List<string> missingAttributes = new List<string>();

         if (string.IsNullOrEmpty(foodItemId))
         {
            missingAttributes.Add("foodItemId");
         }
         if (string.IsNullOrEmpty(name))
         {
            missingAttributes.Add("name");
         }
         if (caloriesPer100g <= 0)
         {
            missingAttributes.Add("caloriesPer100g");
         }
         if (proteinPer100g <= 0)
         {
            missingAttributes.Add("proteinPer100g");
         }
         if (carbsPer100g <= 0)
         {
            missingAttributes.Add("carbsPer100g");
         }
         if (fatPer100g <= 0)
         {
            missingAttributes.Add("fatPer100g");
         }

         return missingAttributes;
      }

      public static List<string> IsValid(string foodItemId, string name, double caloriesPer100g, double proteinPer100g, double carbsPer100g, double fatPer100g)
      {
         List<string> errs = new List<string>();
         errs.Add(IsValidFoodItemId(foodItemId));
         errs.Add(IsValidName(name));
         errs.Add(IsValidCaloriesPer100g(caloriesPer100g));
         errs.Add(IsValidProteinPer100g(proteinPer100g));
         errs.Add(IsValidCarbsPer100g(carbsPer100g));
         errs.Add(IsValidFatPer100g(fatPer100g));

         return errs.Where(s => !string.IsNullOrEmpty(s)).ToList();
      }

      public static List<string> IsValid(FoodItem foodItem)
      {
         return IsValid(
             foodItem.FoodItemId,
             foodItem.Name,
             foodItem.CaloriesPer100g,
             foodItem.ProteinPer100g,
             foodItem.CarbsPer100g,
             foodItem.FatPer100g
         );
      }

      public static FoodItem Create(
          string foodItemId,
          string name,
          double caloriesPer100g,
          double proteinPer100g,
          double carbsPer100g,
          double fatPer100g)
      {
         List<string> missingAttributes = MissingValues(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);
         if (missingAttributes.Any())
         {
            throw new MissingValuesException(missingAttributes);
         }

         List<string> errors = IsValid(
             foodItemId,
             name,
             caloriesPer100g,
             proteinPer100g,
             carbsPer100g,
             fatPer100g);
         if (errors.Any())
         {
            throw new ValueErrorException(errors);
         }

         return new FoodItem(
             foodItemId,
             name,
             caloriesPer100g,
             proteinPer100g,
             carbsPer100g,
             fatPer100g
         );
      }

      public static FoodItem Update(
          string foodItemId,
          string name,
          double? caloriesPer100g,
          double? proteinPer100g,
          double? carbsPer100g,
          double? fatPer100g)
      {
         string idError = IsValidFoodItemId(foodItemId);
         if (!string.IsNullOrEmpty(idError))
         {
            throw new ValueErrorException(new List<string> { idError });
         }

         // Prepare the list of updated values (only those that are not null)
         List<string> errors = new List<string>();

         if (caloriesPer100g.HasValue)
         {
            errors.Add(IsValidCaloriesPer100g(caloriesPer100g.Value));
         }

         if (proteinPer100g.HasValue)
         {
            errors.Add(IsValidProteinPer100g(proteinPer100g.Value));
         }

         if (carbsPer100g.HasValue)
         {
            errors.Add(IsValidCarbsPer100g(carbsPer100g.Value));
         }

         if (fatPer100g.HasValue)
         {
            errors.Add(IsValidFatPer100g(fatPer100g.Value));
         }

         if (errors.Any())
         {
            throw new ValueErrorException(errors.Where(e => !string.IsNullOrEmpty(e)).ToList());
         }

         return new FoodItem(
             foodItemId,
             name ?? "",
             caloriesPer100g ?? 0,
             proteinPer100g ?? 0,
             carbsPer100g ?? 0,
             fatPer100g ?? 0
         );
      }
   }
}
