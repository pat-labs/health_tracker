using Domain.Model;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entity.Mapping;


public static class FoodItemMapper
{
   public static FoodItem AdaptToModel(FoodItemEntity foodItemEntity)
   {
      return new FoodItem
      {
         FoodItemId = foodItemEntity.FoodItemId,
         Name = foodItemEntity.Name,
         CaloriesPer100g = foodItemEntity.CaloriesPer100g,
         ProteinPer100g = foodItemEntity.ProteinPer100g,
         CarbsPer100g = foodItemEntity.CarbsPer100g,
         FatPer100g = foodItemEntity.FatPer100g
      };
   }

   public static FoodItemEntity AdaptToEntity(FoodItem foodItem)
   {
      return new FoodItemEntity
      {
         FoodItemId = foodItem.FoodItemId,
         Name = foodItem.Name,
         CaloriesPer100g = foodItem.CaloriesPer100g,
         ProteinPer100g = foodItem.ProteinPer100g,
         CarbsPer100g = foodItem.CarbsPer100g,
         FatPer100g = foodItem.FatPer100g
      };
   }
}