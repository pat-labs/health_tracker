using Service;
using Service.DrivenAdapter.DatabaseAdapter.Configuration;
using Service.DrivingAdapter.Configuration;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 1. Configuration binding step

ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
AppSettings appSettings = new();
configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

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
builder.Services.AddDatabase(appSettings.DatabaseConnection);

// 4. Use services step

WebApplication app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// 5. Application startup step

app.Run();