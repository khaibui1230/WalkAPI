using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // For EF Core functionalities
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

//Add DbAuthContext
builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkAuthConnectionString"))
);

// Register repository services for dependency injection
builder.Services.AddScoped<IRegionRespositories, SQLRegionResponsitories>();
builder.Services.AddScoped<IWalksRespositories, SQLWalkResponsitories>();

// Add AutoMapper and specify the assembly containing the profiles
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

// Add Identity services to the application, configuring it to use the IdentityUser class for user management
builder.Services.AddIdentityCore<IdentityUser>()
    // Add support for roles using the IdentityRole class
    .AddRoles<IdentityRole>()
    // Add a token provider for generating security tokens, using DataProtectorTokenProvider with a specified name "NZWalks"
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    // Configure Entity Framework to use NZWalksAuthDbContext for storing identity data
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    // Add default token providers for password reset, email confirmation, etc.
    .AddDefaultTokenProviders();

// Configure password policy options for Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    // Do not require a digit in the password
    options.Password.RequireDigit = false;
    // Do not require a lowercase letter in the password
    options.Password.RequireLowercase = false;
    // Do not require an uppercase letter in the password
    options.Password.RequireUppercase = false;
    // Do not require a non-alphanumeric character in the password
    options.Password.RequireNonAlphanumeric = false;
    // Require passwords to be at least 6 characters long
    options.Password.RequiredLength = 6;
    // Require passwords to have at least 1 unique character
    options.Password.RequiredUniqueChars = 1;
});


//Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding
          .UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      });

var app = builder.Build(); // Build the WebApplication

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Check if the environment is development
{
    app.UseSwagger(); // Use Swagger middleware
    app.UseSwaggerUI(); // Use Swagger UI middleware
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.UseAuthentication();

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

app.Run(); // Run the application
