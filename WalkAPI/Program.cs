using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Mapping;
using WalkAPI.Responsitories;
using WalkAPI.Responsity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add db
builder.Services.AddDbContext<NZWalkDbContext>(option => 
option.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkConnectionString"))
);

builder.Services.AddScoped<IRegionRespositories, SQLRegionResponsitories>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
