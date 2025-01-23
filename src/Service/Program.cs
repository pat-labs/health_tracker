var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();

var port = Environment.GetEnvironmentVariable("APP_PORT");
if (!int.TryParse(port, out int appPort))
{
   appPort = 9090; // Default port
}

app.Run($"http://*:{appPort}");