using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;

using Domain.Model;
using Domain.Factory;

namespace Test.Units.Domain
{
   public class FoodItemTest
   {
      [Theory, AutoData]
      public void Create_should_throw_MissingValuesException_when_required_fields_are_missing(string foodItemId, string name))
      {
         // Arrange (incomplete data)
         double caloriesPer100g = 0;
      double proteinPer100g = 0;
      double carbsPer100g = 0;
      double fatPer100g = 0;

      // Act
      Action action = () => FoodItemFactory.Create(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);

      // Assert
      action.Should().Throw<MissingValuesException>();
      }

      [Theory, AutoData]
      public void Create_should_throw_ValueErrorException_when_invalid_values_are_provided(string foodItemId, string name, double caloriesPer100g, double proteinPer100g, double carbsPer100g, double fatPer100g))
      {
         // Arrange (invalid data)
         caloriesPer100g = -10; // Negative calories

         // Act
         Action action = () => FoodItemFactory.Create(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);

   // Assert
   action.Should().Throw<ValueErrorException>();
      }

[Theory, AutoData]
public void Create_should_return_a_valid_FoodItem_with_provided_values(string foodItemId, string name, double caloriesPer100g, double proteinPer100g, double carbsPer100g, double fatPer100g))
      {
   // Arrange (valid data)

   // Act
   FoodItem foodItem = FoodItemFactory.Create(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);

   // Assert
   foodItem.FoodItemId.Should().Be(foodItemId);
   foodItem.Name.Should().Be(name);
   foodItem.CaloriesPer100g.Should().Be(caloriesPer100g);
   foodItem.ProteinPer100g.Should().Be(proteinPer100g);
   foodItem.CarbsPer100g.Should().Be(carbsPer100g);
   foodItem.FatPer100g.Should().Be(fatPer100g);
}

[Theory, AutoData]
public void Update_should_throw_ValueErrorException_when_foodItemId_is_invalid(string invalidFoodItemId, string name, double? caloriesPer100g, double? proteinPer100g, double? carbsPer100g, double? fatPer100g))
      {
   // Arrange

   // Act
   Action action = () => FoodItemFactory.Update(invalidFoodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);

   // Assert
   action.Should().Throw<ValueErrorException>();
}

[Theory, AutoData]
public void Update_should_throw_ValueErrorException_when_any_updated_value_is_invalid(string foodItemId, string name, double? caloriesPer100g, double? proteinPer100g, double? carbsPer100g, double? fatPer100g))
      {
   // Arrange (invalid caloriesPer100g)
   caloriesPer100g = -10;

   // Act
   Action action = () => FoodItemFactory.Update(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);

   // Assert
   action.Should().Throw<ValueErrorException>();
}

[Theory, AutoData]
public void Update_should_return_updated_FoodItem_with_valid_values(string foodItemId, string name, double? caloriesPer100g, double? proteinPer100g, double? carbsPer100g, double? fatPer100g))
      {
   // Arrange (valid data)

   // Act
   FoodItem updatedFoodItem = FoodItemFactory.Update(foodItemId, name, caloriesPer100g, proteinPer100g, carbsPer100g, fatPer100g);

   // Assert
   updatedFoodItem.FoodItemId.Should().Be(foodItemId);
   updatedFoodItem.Name.Should().Be(name);
   updatedFoodItem.CaloriesPer100g.Should().Be(caloriesPer100g ?? 0); // Check if nulls are handled correctly
   updatedFoodItem.ProteinPer100g.Should().Be(proteinPer100g ?? 0);
   updatedFoodItem.CarbsPer100g.Should().Be(carbsPer100g ?? 0);
   updatedFoodItem.FatPer100g.Should().Be(fatPer100g ?? 0);
}
   }
}