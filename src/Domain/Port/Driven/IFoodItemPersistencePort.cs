using Domain.Model;

namespace Domain.Port.Driven;

public interface IFoodItemPersistencePort
{
   Task<List<FoodItem>> FetchAll();
   Task<FoodItem?> FetchById(string id);
   Task Create(FoodItem foodItem);
   Task Update(FoodItem foodItem);
}