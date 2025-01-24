using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;

//using Service;
using Service.DrivingAdapter.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

builder.Services.AddRouting(options =>
{
   options.LowercaseUrls = true;
});
builder.Services.AddUseCases();

var app = builder.Build();

app.MapHealthChecks("/health");
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("APP_PORT");
if (!int.TryParse(port, out int appPort))
{
   appPort = 9090; // Default port
}
app.Run($"http://*:{appPort}");