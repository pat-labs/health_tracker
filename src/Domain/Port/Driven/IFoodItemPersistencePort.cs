using Domain.Model;

namespace Domain.Port.Driven;

public interface IFoodItemPersistencePort
{
   Task<List<FoodItem>> FetchAsync();
   Task<FoodItem?> FetchByIdAsync(string id);
   Task CreateAsync(FoodItem foodItem);
   Task UpdateAsync(FoodItem foodItem);
   Task DeleteAsync(string foodItemId);
}