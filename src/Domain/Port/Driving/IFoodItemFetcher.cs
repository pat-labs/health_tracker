using Domain.Model;

namespace Domain.Port.Driving;

public interface IFoodItemFetcher
{
    Task<List<FoodItem>> Execute();
    Task<FoodItem?> Execute(string foodItemId);
}