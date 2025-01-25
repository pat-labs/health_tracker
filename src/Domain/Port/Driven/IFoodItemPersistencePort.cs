using System.Linq;

using Domain.Model;

namespace Domain.Port.Driven;


public interface IFoodItemPersistencePort
{
   Task<IEnumerable<FoodItem>> FetchAsync();
   Task<FoodItem?> FetchByIdAsync(string foodItemId);
   Task CreateAsync(FoodItem foodItem);
   Task UpdateAsync(FoodItem foodItem);
   Task DeleteAsync(string foodItemId);
}