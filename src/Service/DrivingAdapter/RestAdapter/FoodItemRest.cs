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
   private readonly IFoodItemUseCase _foodItemUseCase;

   public FoodItemRest(IFoodItemUseCase foodItemUseCase)
   {
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
      return foodItems.Select(foodItem => FoodItemMapper.AdaptToDto(foodItem)).ToList();
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
      return Ok(FoodItemMapper.AdaptToDto(foodItem.Value));
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
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status400BadRequest)]
   public async Task<ActionResult<FoodItemDto>> Update(string foodItemId, [FromBody] FoodItemDto foodItemDto)
   {
      if (foodItemId != foodItemDto.FoodItemId)
      {
         return BadRequest("Food item ID in the URL and body don't match");
      }
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      FoodItem foodItem = FoodItemMapper.AdaptToModel(foodItemDto);
      List<string> errors = FoodItemFactory.IsValid(foodItem);
      if (errors.Any())
      {
         return BadRequest(string.Join("\n", errors));
      }

      await _foodItemUseCase.Update(foodItem);
      return foodItemDto;
   }

   [HttpPost]
   [ProducesResponseType(typeof(string), Status201Created)]
   [ProducesResponseType(typeof(string), Status400BadRequest)]
   public async Task<ActionResult<FoodItemDto>> Create([FromBody] FoodItemDto foodItemDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      FoodItem foodItem = FoodItemMapper.AdaptToModel(foodItemDto);
      List<string> errors = FoodItemFactory.IsValid(foodItem);
      if (errors.Any())
      {
         return BadRequest(string.Join("\n", errors));
      }

      await _foodItemUseCase.Create(foodItem);
      return Ok("OK");
   }
}