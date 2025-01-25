using Domain.Model;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entity.Mapping;


public static class FoodItemMapper
{
   public static FoodItem AdaptToModel(FoodItemEntity foodItemEntity)
   {
      return new FoodItem
      {
         FoodItemId = foodItemEntity.food_item_id,
         Name = foodItemEntity.name,
         CaloriesPer100g = foodItemEntity.calories_per_100g,
         ProteinPer100g = foodItemEntity.protein_per_100g,
         CarbsPer100g = foodItemEntity.carbs_per_100g,
         FatPer100g = foodItemEntity.fat_per_100g
      };
   }

   public static FoodItemEntity AdaptToEntity(FoodItem foodItem)
   {
      return new FoodItemEntity
      {
         food_item_id = foodItem.FoodItemId,
         name = foodItem.Name,
         calories_per_100g = foodItem.CaloriesPer100g,
         protein_per_100g = foodItem.ProteinPer100g,
         carbs_per_100g = foodItem.CarbsPer100g,
         fat_per_100g = foodItem.FatPer100g
      };
   }
}