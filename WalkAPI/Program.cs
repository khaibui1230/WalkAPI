using Microsoft.EntityFrameworkCore; // For EF Core functionalities
using WalkAPI.Data; // Namespace for database context
using WalkAPI.Mapping; // Namespace for AutoMapper profiles
using WalkAPI.Responsitories; // Namespace for repository interfaces
using WalkAPI.Responsity; // Namespace for repository implementations

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Add controller services to the DI container

// Add Swagger for API documentation
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Add endpoints for API explorer
builder.Services.AddSwaggerGen(); // Add Swagger generation services

// Add DbContext with SQL Server configuration
builder.Services.AddDbContext<NZWalkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkConnectionString"))
);

// Register repository services for dependency injection
builder.Services.AddScoped<IRegionRespositories, SQLRegionResponsitories>();
builder.Services.AddScoped<IWalksRespositories, SQLWalkResponsitories>();

// Add AutoMapper and specify the assembly containing the profiles
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

var app = builder.Build(); // Build the WebApplication

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Check if the environment is development
{
    app.UseSwagger(); // Use Swagger middleware
    app.UseSwaggerUI(); // Use Swagger UI middleware
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

app.Run(); // Run the application
