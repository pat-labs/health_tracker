using System.Linq;

using Domain.Except;
using Domain.Model;
using Domain.Port.Driven;
using Domain.Port.Driving;

namespace Domain.UseCase;

public class FoodItemUseCase : IFoodItemUseCase
{
   private readonly IFoodItemPersistencePort _foodItemPersistencePort;

   public FoodItemUseCase(IFoodItemPersistencePort foodItemPersistencePort)
   {
      _foodItemPersistencePort = foodItemPersistencePort;
   }

   public async Task<IEnumerable<FoodItem>> Fetch()
   {
      return await _foodItemPersistencePort.FetchAsync();
   }

   public async Task<FoodItem?> FetchById(string foodItemId)
   {
      return await _foodItemPersistencePort.FetchByIdAsync(foodItemId);
   }

   public async Task Create(FoodItem foodItem)
   {
      await _foodItemPersistencePort.CreateAsync(foodItem);
   }

   public async Task Update(FoodItem foodItem)
   {
      await _foodItemPersistencePort.UpdateAsync(foodItem);
   }

   public async Task Delete(string foodItemId)
   {
      await _foodItemPersistencePort.DeleteAsync(foodItemId);
   }
}