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
public class FoodItem : ControllerBase
{
   private readonly IMapper _mapper;
   private readonly IFoodItemUseCase _foodItemUseCase;

   public FoodItem(IMapper mapper, IFoodItemUseCase foodItemUseCase)
   {
      _mapper = mapper;
      _foodItemUseCase = foodItemUseCase;
   }

   [HttpGet()]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<FoodItemDto> Fetch()
   {
      List<FoodItem?> foodItems = await _foodItemUseCase.Fetch();
      return _mapper.Map<FoodItemDto>(foodItems);
   }

   [HttpGet("{foodItemId:int:required}")]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<FoodItemDto> FetchById(string foodItemId)
   {
      FoodItem foodItem = await _foodItemUseCase.FetchById(foodItemId);
      return _mapper.Map<FoodItemDto>(foodItem);
   }

   [HttpPost]
   [ProducesResponseType(typeof(FoodItemDto), Status201Created)]
   [ProducesResponseType(typeof(void), Status400BadRequest)]
   public async Task<ActionResult<FoodItemDto>> Create([FromBody] FoodItemDto foodItemDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      FoodItem foodItem = _mapper.Map<FoodItem>(foodItemDto);
      FoodItem createdFoodItem = await _foodItemUseCase.Create(foodItem);

      if (createdFoodItem == null)
      {
         return StatusCode(500, "Internal Server Error"); // Or a more specific error message
      }

      return CreatedAtRoute("GetFoodItem", new { foodItemId = createdFoodItem.Id }, _mapper.Map<FoodItemDto>(createdFoodItem));
   }

   [HttpPut("{foodItemId:int:required}")]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   [ProducesResponseType(typeof(void), Status400BadRequest)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<ActionResult<FoodItemDto>> Update(int foodItemId, [FromBody] FoodItemDto foodItemDto)
   {
      if (foodItemId != foodItemDto.Id)
      {
         return BadRequest("Food item ID in the URL and body don't match");
      }

      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      FoodItem foodItem = _mapper.Map<FoodItem>(foodItemDto);
      FoodItem updatedFoodItem = await _foodItemUseCase.Update(foodItemId, foodItem);

      if (updatedFoodItem == null)
      {
         return NotFound();
      }

      return _mapper.Map<FoodItemDto>(updatedFoodItem);
   }

   [HttpDelete("{foodItemId:int:required}")]
   [ProducesResponseType(typeof(void), Status204NoContent)]
   [ProducesResponseType(typeof(void), Status404NotFound)]
   public async Task<IActionResult> Delete(int foodItemId)
   {
      await _foodItemUseCase.Delete(foodItemId);
      return NoContent();
   }
}
