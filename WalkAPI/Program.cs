using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // For EF Core functionalities
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.NetworkInformation;
using System.Text;
using WalkAPI.Data; // Namespace for database context
using WalkAPI.Mapping; // Namespace for AutoMapper profiles
using WalkAPI.Responsitories; // Namespace for repository interfaces
using WalkAPI.Responsity; // Namespace for repository implementations

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Add controller services to the DI container
builder.Services.AddHttpContextAccessor(); //Sign up the http context

// Add Swagger for API documentation
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Add endpoints for API explorer
// Add Swagger for API documentation
// Học thêm về cách cấu hình Swagger/OpenAPI tại https://aka.ms/aspnetcore/swashbuckle

// Thêm explorer cho các endpoints của API
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Tạo một tài liệu Swagger mới với tiêu đề "NZ Walks Api" và phiên bản "v1"
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Walks Api", Version = "v1" });

    // Thêm định nghĩa bảo mật cho JWT Bearer Token
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        // Tên của tiêu đề HTTP sẽ chứa token
        Name = "Authorization",

        // Vị trí của tiêu đề HTTP (trong trường hợp này là Header)
        In = ParameterLocation.Header,

        // Loại sơ đồ bảo mật là API Key (trong trường hợp này là JWT)
        Type = SecuritySchemeType.ApiKey,

        // Sơ đồ bảo mật sẽ sử dụng Bearer tokens
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    // Thêm yêu cầu bảo mật cho các endpoint API
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            // Tạo một tham chiếu đến định nghĩa bảo mật đã thêm ở trên
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    // Loại tham chiếu là sơ đồ bảo mật
                    Type = ReferenceType.SecurityScheme,
                    
                    // ID của sơ đồ bảo mật là JWT Bearer
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                
                // Đặt sơ đồ thành "Bearer" để phù hợp với JWT
                Scheme = "Bearer",
                
                // Tên của sơ đồ bảo mật
                Name = JwtBearerDefaults.AuthenticationScheme,
                
                // Vị trí của tiêu đề HTTP là Header
                In = ParameterLocation.Header
            },
            
            // Tạo một danh sách rỗng của các phạm vi bảo mật
            new List<string>()
        }
    });
}); // Thêm các dịch vụ tạo tài liệu Swagger


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
builder.Services.AddScoped<ITokenResponsitory, TokenResponsitory>();
builder.Services.AddScoped<IImageRepository, LocalImageResponsitory>();

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

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        RequestPath = "/Images"
    });

app.MapControllers(); // Map controller routes

app.Run(); // Run the application
