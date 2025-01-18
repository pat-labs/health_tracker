using AutoMapper;
using Domain.Model;
using Domain.Port.Driving;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Service.DrivingAdapter.RestAdapter.Dto;

namespace Service.DrivingAdapter.RestAdapter;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("food_item")]
public class FoodItem: ControllerBase
{
   private readonly IMapper _mapper;
   private readonly IFoodItemFetcher _foodItemFetcher;

   public FoodItem(IMapper mapper, IFoodItemFetcher foodItemFetcher)
   {
      _mapper = mapper;
      _foodItemFetcher = foodItemFetcher;
   }

   [HttpGet()]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<FoodItemDto> Get()
   {
      List<FoodItem?> foodItems = await _foodItemFetcher.Execute();
      return _mapper.Map<FoodItemDto>(foodItems);
   }

   [HttpGet("{foodItemId:int:required}")]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<FoodItemDto> Get(string FoodItemId)
   {
      FoodItem foodItem = await _foodItemFetcher.Execute(foodItemId);

      return _mapper.Map<FoodItemDto>(foodItem);
   }
}
