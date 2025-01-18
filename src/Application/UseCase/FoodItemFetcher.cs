using Domain.Exceptions;
using Domain.Model;
using Domain.Port.Driven;
using Domain.Port.Driving;

namespace Application.UseCases;

public class FoodItemFetcher : IFoodItemFetcher
{
    private readonly IFoodItemPersistencePort _foodItemPersistencePort;

    public FoodItemFetcher(IFoodItemPersistencePort foodItemPersistencePort)
    {
        _foodItemPersistencePort = foodItemPersistencePort;
    }

    public async Task<List<FoodItem>> Execute()
    {
        return await _foodItemPersistencePort.FetchAll();
    }
    public async Task<FoodItem?> Execute(Guid foodItemId)
    {
        return await _foodItemPersistencePort.FetchById(foodItemId);
    }
}