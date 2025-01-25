using Microsoft.EntityFrameworkCore;

using Domain.Model;
using Domain.Port.Driven;
using Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entity;
using Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entity.Mapping;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.UseCase;


public class FoodItemPersistenceAdapter : IFoodItemPersistencePort
{
   private readonly ApplicationDbContext _applicationDbContext;

   public FoodItemPersistenceAdapter(ApplicationDbContext ApplicationDbContext)
   {
      _applicationDbContext = ApplicationDbContext;
   }

   public async Task CreateAsync(FoodItem foodItem)
   {
      var entity = await _applicationDbContext.FoodItems.FindAsync(foodItem.FoodItemId);

      if (entity == null)
      {
         var foodItemEntity = FoodItemMapper.AdaptToEntity(foodItem);
         _applicationDbContext.FoodItems.Add(foodItemEntity);
         await _applicationDbContext.SaveChangesAsync();
      }
   }

   public async Task<FoodItem?> FetchByIdAsync(string foodItemId)
   {
      FoodItemEntity? foodItemEntity = await _applicationDbContext.FoodItems
         .Where(rule => rule.food_item_id == foodItemId)
         .AsNoTracking()
         .FirstOrDefaultAsync();
      return foodItemEntity != null ? FoodItemMapper.AdaptToModel(foodItemEntity) : null;
   }

   public async Task<IEnumerable<FoodItem>> FetchAsync()
   {
      var foodItemEntities = await _applicationDbContext.FoodItems
        .Where(entity => entity != null)
        .ToListAsync();

      return foodItemEntities.Select(entity => FoodItemMapper.AdaptToModel(entity));
   }

   public async Task UpdateAsync(FoodItem foodItem)
   {
      var entity = await _applicationDbContext.FoodItems.FindAsync(foodItem.FoodItemId);

      if (entity != null)
      {
         var foodItemEntity = FoodItemMapper.AdaptToEntity(foodItem);
         _applicationDbContext.FoodItems.Update(foodItemEntity);
         await _applicationDbContext.SaveChangesAsync();
      }
   }

   public async Task DeleteAsync(string foodItemId)
   {
      var entity = await _applicationDbContext.FoodItems.FindAsync(foodItemId);

      if (entity != null)
      {
         _applicationDbContext.FoodItems.Remove(entity);
         await _applicationDbContext.SaveChangesAsync();
      }
   }
}