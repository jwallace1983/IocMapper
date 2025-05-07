using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

// Build
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddIocMappings()
    .AddMediator()
    .AddControllers();

// Configure
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run
app.Run();
