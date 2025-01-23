using Microsoft.AspNetCore.Diagnostics.HealthChecks;

using Service;
using Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Configuration;
using Service.DrivingAdapter.Configuration;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 1. Configuration binding step

ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
AppSettings appSettings = new();
configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

// Basic health check
builder.Services.AddHealthChecks();

// 2. Configure logging from AppSettings
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

builder.Logging.AddConfiguration(configuration.GetSection("Logging"));

// 3. Add services step

builder.Services.AddControllers(options =>
{
   options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
builder.Services.AddRouting(options =>
{
  // Add your constraint registration here
  options.ConstraintMap.Add("string", typeof(string)); // Example constraint registration
  options.LowercaseUrls = true;
});
builder.Services.AddUseCases();
// builder.Services.AddThirdParties(appSettings);
builder.Services.AddAutoMapper(Assembly.Load(typeof(Program).Assembly.GetName().Name!));
try
{
   var connectionString = $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")}:{Environment.GetEnvironmentVariable("POSTGRES_PORT")};Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};User Id={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";
   builder.Services.AddDatabase(connectionString);
}
catch (Exception ex)
{
   // Handle the exception appropriately, log the error, or throw a more specific exception
   Console.WriteLine($"Error configuring database connection: {ex.Message}");
}

// 4. Use services step

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // Add OpenAPI 3.0 document serving middleware
    // Available at: http://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();

    // Add web UIs to interact with the document
    // Available at: http://localhost:<port>/swagger
    app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
}

app.MapHealthChecks("/health");

app.UseDeveloperExceptionPage();
app.UseRouting();
app.MapControllers();

RoutePrinter.LogRoutes(app);

// 5. Application startup step

var port = Environment.GetEnvironmentVariable("APP_PORT");
if (!int.TryParse(port, out int appPort))
{
    appPort = 9090; // Default port
}

app.Run($"http://*:{appPort}");