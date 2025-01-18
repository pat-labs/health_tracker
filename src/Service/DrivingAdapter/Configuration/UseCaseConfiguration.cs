using Domain.Port.Driving;
using Domain.UseCase;

namespace Service.DrivingAdapter.Configuration;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        services.AddTransient<IFoodItemFetcher, FoodItemFetcher>();

        return services;
    }
}
