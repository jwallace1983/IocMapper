using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Samples.Microsoft.DependencyInjection.WebApiNet6.Services;

// Build
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddIocMappings(typeof(IWeatherService)) // Add custom mappings (optionally provide file from any binaries needed)
    .AddControllers();

// Configure
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run
app.Run();
