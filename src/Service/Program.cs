using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

//using Service;
using Service.DrivingAdapter.Configuration;
using Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Configuration;
using Service.DrivenAdapter.Middleware.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

builder.Services.AddRouting(options =>
{
   options.LowercaseUrls = true;
});
builder.Services.AddControllers();
builder.Services.AddUseCases();
try
{
   var connectionString = $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")}:{Environment.GetEnvironmentVariable("POSTGRES_PORT")};Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};User Id={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";
   builder.Services.AddDatabase(connectionString);
}
catch (Exception ex)
{
   Console.WriteLine($"Error configuring database connection: {ex.Message}");
}

var app = builder.Build();

app.MapHealthChecks("/health");
if (bool.TryParse(Environment.GetEnvironmentVariable("IS_IN_PRODUCTION") ?? "false", out bool isProduction) && isProduction)
{
   app.MapOpenApi();
   app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.UseWriteUidMiddleware();

var port = Environment.GetEnvironmentVariable("APP_PORT");
if (!int.TryParse(port, out int appPort))
{
   appPort = 9090;
}
app.Run($"http://*:{appPort}");