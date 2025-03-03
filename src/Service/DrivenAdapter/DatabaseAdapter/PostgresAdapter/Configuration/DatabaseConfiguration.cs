using Microsoft.EntityFrameworkCore;

using Domain.Port.Driven;
using Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.UseCase;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Configuration;

public static class DatabaseAdapterConfiguration
{
   public static IServiceCollection AddDatabase(this IServiceCollection services, string databaseConnection)
   {
      services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(databaseConnection).UseSnakeCaseNamingConvention());

      services.AddTransient<IFoodItemPersistencePort, FoodItemPersistenceAdapter>();

      return services;
   }
}