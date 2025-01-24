using System.Linq;

using Domain.Model;

namespace Domain.Port.Driving;


public interface IFoodItemUseCase
{
   Task<IEnumerable<FoodItem>> Fetch();
   Task<FoodItem?> FetchById(string foodItemId);
   Task Delete(string foodItemId);
   Task Create(FoodItem foodItem);
   Task Update(FoodItem foodItem);
}