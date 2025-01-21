using Domain.Model;

namespace Domain.Port.Driving;

public interface IFoodItemUseCase
{
    Task<List<FoodItem>> Fetch();
    Task<FoodItem?> FetchById(string foodItemId);
    Task Create(FoodItem foodItem);
    Task Update(FoodItem foodItem);
}