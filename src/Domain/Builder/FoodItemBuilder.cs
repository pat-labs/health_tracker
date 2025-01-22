using System.Collections.Generic;
using System.Linq;

namespace Domain.Builder;

public class FoodItemBuilder
{
    public string IsValidFoodItemId(string foodItemId)
    {
        string error = "";
        if (string.IsNullOrEmpty(foodItemId) || foodItemId.Length > 19) // Corrected logic and added OR condition
        {
            error = "Id could not be empty and the max length is 19"; // Corrected message
        }
        return error;
    }

    public string IsValidName(string name) // Added parameter
    {
        if (string.IsNullOrEmpty(name) || name.Length > 200)
        {
            return "Name could not be empty and the max length is 200";
        }
        return "";
    }

    public string IsValidCaloriesPer100g(double caloriesPer100g) // Added parameter
    {
        if (caloriesPer100g < 0)
        {
            return "Calories must be positive";
        }
        return "";
    }

    public string IsValidProteinPer100g(double proteinPer100g) // Added parameter
    {
        if (proteinPer100g < 0)
        {
            return "Protein must be positive";
        }
        return "";
    }

    public string IsValidCarbsPer100g(double carbsPer100g) // Added parameter
    {
        if (carbsPer100g < 0)
        {
            return "Carbs must be positive";
        }
        return "";
    }

    public string IsValidFatPer100g(double fatPer100g) // Added parameter
    {
        if (fatPer100g < 0)
        {
            return "Fat must be positive";
        }
        return "";
    }

    public List<string> IsValid(
        string foodItemId,
        string name,
        double caloriesPer100g,
        double proteinPer100g,
        double carbsPer100g,
        double fatPer100g)
    {
        List<string> errs = new List<string>(); // Corrected type and initialization
        errs.Add(IsValidFoodItemId(foodItemId));
        errs.Add(IsValidName(name));
        errs.Add(IsValidCaloriesPer100g(caloriesPer100g));
        errs.Add(IsValidProteinPer100g(proteinPer100g));
        errs.Add(IsValidCarbsPer100g(carbsPer100g));
        errs.Add(IsValidFatPer100g(fatPer100g));

        return errs.Where(s => !string.IsNullOrEmpty(s)).ToList(); // Filter empty errors using LINQ
    }

    public FoodItem NewFoodItem(
        string foodItemId,
        string name,
        double caloriesPer100g,
        double proteinPer100g,
        double carbsPer100g,
        double fatPer100g)
    {
        List<string> errors = IsValid(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g); // Corrected type
        if (errors.Any()) // Use Any() to check if the list has elements
        {
            throw new DomainException(string.Join("\n", errors)); // Join errors with newline
        }
        return new FoodItem(
            foodItemId,
            name,
            caloriesPer100g,
            proteinPer100g,
            carbsPer100g,
            fatPer100g);
    }
}