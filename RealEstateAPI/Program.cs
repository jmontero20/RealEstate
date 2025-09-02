using RealEstate.Application;
using RealEstate.Infrastructure;
using RealEstateAPI.Filters;
using RealEstateAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddScoped<ValidationFilter>();

// Add layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add Swagger (siempre presente)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure pipeline
app.UseMiddleware<GlobalExceptionHandler>();

// Swagger siempre disponible
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Initialize database
await app.Services.MigrateAndSeedDatabaseAsync();

app.Run();