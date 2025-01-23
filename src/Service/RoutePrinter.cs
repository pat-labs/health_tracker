using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing.Patterns;

public static class RoutePrinter
{
    public static void LogRoutes(IEndpointRouteBuilder endpoints)
    {
        foreach (var endpoint in endpoints.DataSources.SelectMany(d => d.Endpoints))
        {
            if (endpoint is RouteEndpoint routeEndpoint)
            {
                Console.WriteLine($"Route: {routeEndpoint.RoutePattern.RawText}, Name: {routeEndpoint.DisplayName}");
            }
        }
    }
}