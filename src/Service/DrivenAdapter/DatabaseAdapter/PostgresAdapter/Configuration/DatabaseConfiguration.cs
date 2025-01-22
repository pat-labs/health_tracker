using Domain.Port.Driven;
using Microsoft.EntityFrameworkCore;
using Service.DrivenAdapter.DatabaseAdapter.Migrations;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Configuration;

public static class DatabaseAdapterConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string databaseConnection)
    {
        services.AddDbContext<FoodItemContext>(options => options.UseLazyLoadingProxies().UseNpgsql(databaseConnection).UseSnakeCaseNamingConvention());
        services.AddHostedService<MigratorHostedService>();

        services.AddTransient<IFoodItemPersistencePort, FoodItemPersistenceAdapter>();

        return services;
    }
}