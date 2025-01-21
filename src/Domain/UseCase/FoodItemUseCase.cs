
using Domain.Except;
using Domain.Model;
using Domain.Port.Driven;
using Domain.Port.Driving;

namespace Application.UseCases;

public class FoodItemUseCase {
    private readonly IFoodItemPersistencePort _foodItemPersistencePort;

    public async Task<List<FoodItem>> Fetch() 
    {
        return await _foodItemPersistencePort.FetchAll();
    }

    public async Task<FoodItem?> FetchById(string foodItemId)
    {
        return await _foodItemPersistencePort.FetchById(foodItemId);
    }

    public async void Create(FoodItem foodItem)
    {
        _foodItemPersistencePort.Create(foodItem);
    }

    public async void Update(FoodItem foodItem)
    {
        _foodItemPersistencePort.Update(foodItem);
    }
}