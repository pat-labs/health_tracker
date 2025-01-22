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

builder.Logging.AddConfiguration(configuration.GetSection("Logging"));

// 3. Add services step

builder.Services.AddControllers(options =>
{
   options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
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

app.UseDeveloperExceptionPage();
app.UseRouting();
app.MapControllers();
app.MapHealthChecks("/health");

// 5. Application startup step

app.Run();