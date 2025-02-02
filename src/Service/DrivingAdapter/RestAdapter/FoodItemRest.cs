using Serilog;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

using Domain.Factory;
using Domain.Model;
using Domain.Port.Driving;
using Service.DrivingAdapter.RestAdapter.Dto;
using Service.DrivingAdapter.RestAdapter.Dto.Mapping;

namespace Service.DrivingAdapter.RestAdapter;


[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("food_item")]
public class FoodItemRest : ControllerBase
{
   private readonly ILogger<FoodItemRest> _logger;
   private readonly IMapper _mapper;
   private readonly IFoodItemUseCase _foodItemUseCase;

   public FoodItemRest(ILogger<FoodItemRest> logger, IMapper mapper, IFoodItemUseCase foodItemUseCase)
   {
      _logger = logger;
      _mapper = mapper;
      _foodItemUseCase = foodItemUseCase;
   }


   [HttpGet]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<List<FoodItemDto>> Fetch()
   {
      IEnumerable<FoodItem> foodItems = await _foodItemUseCase.Fetch();
      if (!foodItems.Any())
      {
         return new List<FoodItemDto>();
      }

      return foodItems.Select(foodItem => _mapper.Map<FoodItemDto>(foodItem)).ToList();
   }

   [HttpGet("{foodItemId}")]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<FoodItemDto>> FetchById(string foodItemId)
   {
      string validationError = FoodItemFactory.IsValidFoodItemId(foodItemId);
      if (!string.IsNullOrEmpty(validationError))
      {
         return BadRequest(validationError);
      }

      FoodItem? foodItem = await _foodItemUseCase.FetchById(foodItemId);
      if (foodItem == null)
      {
         return NotFound();
      }

      return Ok(_mapper.Map<FoodItemDto>(foodItem.Value));
   }

   [HttpDelete("{foodItemId}")]
   [ProducesResponseType(typeof(void), Status204NoContent)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<IActionResult> Delete(string foodItemId)
   {
      string validationError = FoodItemFactory.IsValidFoodItemId(foodItemId);
      if (!string.IsNullOrEmpty(validationError))
      {
         return BadRequest(validationError);
      }

      await _foodItemUseCase.Delete(foodItemId);

      return NoContent();
   }

   [HttpPut("{foodItemId}")]
   [ProducesResponseType(typeof(string), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   [ProducesResponseType(typeof(void), Status400BadRequest)]
   public async Task<ActionResult<string>> Update(string foodItemId, [FromBody] FoodItemDto updateFoodItemDto)
   {
      _logger.LogError("UPDATE");
      string validationError = FoodItemFactory.IsValidFoodItemId(foodItemId);
      if (!string.IsNullOrEmpty(validationError))
      {
         return BadRequest(validationError);
      }

      FoodItem? foodItem = await _foodItemUseCase.FetchById(foodItemId);
      if (foodItem == null)
      {
         return NotFound();
      }

      FoodItem updatedFoodItem = foodItem.Value with
      {
         Name = updateFoodItemDto.Name ?? foodItem.Value.Name,
         CaloriesPer100g = updateFoodItemDto.CaloriesPer100g ?? foodItem.Value.CaloriesPer100g,
         ProteinPer100g = updateFoodItemDto.ProteinPer100g ?? foodItem.Value.ProteinPer100g,
         CarbsPer100g = updateFoodItemDto.CarbsPer100g ?? foodItem.Value.CarbsPer100g,
         FatPer100g = updateFoodItemDto.FatPer100g ?? foodItem.Value.FatPer100g
      };
      _logger.LogError("new foodItem: " + updatedFoodItem);

      List<string> errors = FoodItemFactory.IsValid(updatedFoodItem);
      if (errors.Any())
      {
         return BadRequest(string.Join("\n", errors));
      }

      await _foodItemUseCase.Update(updatedFoodItem);

      return Ok("OK");
   }

   [HttpPost]
   [ProducesResponseType(typeof(string), Status201Created)]
   [ProducesResponseType(typeof(string), Status400BadRequest)]
   public async Task<ActionResult<string>> Create([FromBody] FoodItemDto newFoodItemDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      FoodItem foodItem = _mapper.Map<FoodItem>(newFoodItemDto);
      List<string> errors = FoodItemFactory.IsValid(foodItem);
      if (errors.Any())
      {
         return BadRequest(string.Join("\n", errors));
      }

      await _foodItemUseCase.Create(foodItem);

      return Ok("OK");
   }
}