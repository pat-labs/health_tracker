using AutoMapper;
using Domain.Model;
using Domain.Port.Driven;
using Microsoft.EntityFrameworkCore;
using Service.DrivenAdapter.DatabaseAdapter.Entities;

namespace Service.DrivenAdapter.DatabaseAdapter;

public class FoodItemPersistenceAdapter : IFoodItemPersistencePort
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public FoodItemPersistenceAdapter(ApplicationDbContext ApplicationDbContext, IMapper mapper)
    {
        _applicationDbContext = ApplicationDbContext;
        _mapper = mapper;
    }

    public async Task CreateAsync(FoodItem foodItem)
    {
        var foodItemEntity = _mapper.Map<FoodItemEntity>(foodItem);
        _applicationDbContext.FoodItems.Add(foodItemEntity);
        await _applicationDbContext.SaveChangesAsync();
        return _mapper.Map<FoodItem>(foodItemEntity);
    }

    public async Task<FoodItem?> FetchByIdAsync(string foodItemId)
    {
        FoodItemEntity? foodItemEntity = await _applicationDbContext.FoodItems.Where(rule => rule.Id == foodItemId)
                                                       .SingleOrDefaultAsync();
        return foodItemEntity != null ? _mapper.Map<FoodItem>(foodItemEntity) : null;
    }

    public async Task<IEnumerable<FoodItem>> FetchAsync()
    {
        var foodItemEntities = await _applicationDbContext.FoodItems.ToListAsync();
        return foodItemEntities.Select(entity => _mapper.Map<FoodItem>(entity));
    }

    public async Task UpdateAsync(FoodItem foodItem)
    {
        var foodItemEntity = await _applicationDbContext.FoodItems.FindAsync(foodItem.Id);

        if (foodItemEntity != null)
        {
            _mapper.Map(foodItem, foodItemEntity);
            _applicationDbContext.FoodItems.Update(foodItemEntity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(string foodItemId)
    {
        var foodItemEntity = await _applicationDbContext.FoodItems.FindAsync(foodItemId);

        if (foodItemEntity != null)
        {
            _applicationDbContext.FoodItems.Remove(foodItemEntity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}