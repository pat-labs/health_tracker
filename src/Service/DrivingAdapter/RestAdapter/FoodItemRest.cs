using Domain.Builder;
using Domain.Model;
//using Domain.Port.Driving;
using Service.DrivingAdapter.RestAdapter.Dto;
using Service.DrivingAdapter.RestAdapter.Dto.Mapping;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Service.DrivingAdapter.RestAdapter;


[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("food_item")]
public class FoodItemRest : ControllerBase
{
   [HttpGet()]
   [ProducesResponseType(typeof(FoodItemDto), Status200OK)]
   public async Task<FoodItemDto> Fetch()
   {
      FoodItem foodItem = FoodItemBuilder.NewFoodItem("4f0d17ef-489d-4644-91b2-151034794d41", "Example Food", 200.50, 10.25, 25.75, 5.00);
      return FoodItemMapper.AdaptToDto(foodItem);
   }
}