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

    public async Task<FoodItem?> GetById(Guid foodItemId)
    {
        FoodItemEntity? foodItem = await _applicationDbContext.FoodItems.Where(rule => rule.Id == foodItemId)
                                                       .SingleOrDefaultAsync();

        return foodItem != null ? _mapper.Map<FoodItem>(foodItem) : null;
    }
}