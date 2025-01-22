using System.Linq;

using AutoMapper;
using Domain.Model;
using Domain.Builder;
using Domain.Port.Driving;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

using Service.DrivingAdapter.RestAdapter.Dto;

namespace Service.DrivingAdapter.RestAdapter;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("food_item")]
public class FoodItemRest : ControllerBase
{
   private readonly FoodItemBuilder _foodItemBuilder;
   private readonly IMapper _mapper;
   private readonly IFoodItemUseCase _foodItemUseCase;

   public FoodItemRest(FoodItemBuilder foodItemBuilder, IMapper mapper, IFoodItemUseCase foodItemUseCase)
   {
      _foodItemBuilder = foodItemBuilder;
      _mapper = mapper;
      _foodItemUseCase = foodItemUseCase;
   }


   [HttpGet()]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   public async Task<List<FoodItemDto>> Fetch()
   {
      IEnumerable<FoodItem?> foodItems = await _foodItemUseCase.Fetch();

      if (!foodItems.Any())
      {
         return new List<FoodItemDto>();
      }
      return foodItems.Select(foodItem => _mapper.Map<FoodItemDto>(foodItem)).ToList();
   }

   [HttpGet("{foodItemId:string:required}")]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<ActionResult<FoodItemDto>> FetchById(string foodItemId)
   {
      string validationError = _foodItemBuilder.IsValidFoodItemId(foodItemId);
      if (!string.IsNullOrEmpty(validationError))
      {
         return BadRequest(validationError); // Return a 400 Bad Request
      }

      FoodItem? foodItem = await _foodItemUseCase.FetchById(foodItemId);

      if (foodItem == null)
      {
         return NotFound();
      }
      return Ok(_mapper.Map<FoodItemDto>(foodItem));
   }

   [HttpDelete("{foodItemId:string:required}")]
   [ProducesResponseType(typeof(void), Status204NoContent)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<IActionResult> Delete(string foodItemId)
   {
      string validationError = _foodItemBuilder.IsValidFoodItemId(foodItemId);
      if (!string.IsNullOrEmpty(validationError))
      {
         return BadRequest(validationError); // Return a 400 Bad Request
      }

      await _foodItemUseCase.Delete(foodItemId);
      return NoContent();
   }

   [HttpPut("{foodItemId:string:required}")]
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

      string validationError = _foodItemBuilder.IsValidFoodItemId(foodItemId);
      if (!string.IsNullOrEmpty(validationError))
      {
         return BadRequest(validationError); // Return a 400 Bad Request
      }

      FoodItem foodItem = _mapper.Map<FoodItem>(foodItemDto);
      await _foodItemUseCase.Update(foodItem);

      return foodItemDto;
   }

   [HttpPost]
   [ProducesResponseType(typeof(string), Status201Created)]
   [ProducesResponseType(typeof(string), Status400BadRequest)] // Changed return type to string for error message
   public async Task<ActionResult<FoodItemDto>> Create([FromBody] FoodItemDto foodItemDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      List<string> errors = _foodItemBuilder.IsValid(
        foodItemDto.FoodItemId,
        foodItemDto.Name,
        foodItemDto.CaloriesPer100g,
        foodItemDto.ProteinPer100g,
        foodItemDto.CarbsPer100g,
        foodItemDto.FatPer100g);

      if (errors.Any())
      {
         return BadRequest(string.Join("\n", errors));
      }

      FoodItem foodItem = _mapper.Map<FoodItem>(foodItemDto);
      await _foodItemUseCase.Create(foodItem);
      return Ok("OK");
   }

}
