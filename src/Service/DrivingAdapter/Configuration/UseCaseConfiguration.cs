using Domain.Port.Driving;
using Domain.UseCase;

namespace Service.DrivingAdapter.Configuration;

public static class UseCaseConfiguration
{
   public static IServiceCollection AddUseCases(this IServiceCollection services)
   {
      services.AddTransient<IFoodItemUseCase, FoodItemUseCase>();

      return services;
   }
}
