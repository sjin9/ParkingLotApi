using ParkingLotApi.Exceptions;
using ParkingLotApi.Filters;
using ParkingLotApi.Models;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<InvalidCapacityExceptionFilter>(); //inject filter in program
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ParkingLotsService>();
builder.Services.AddSingleton<IParkingLotsRepository, ParkingLotsRepository>(); //singleton bc 希望只是一份数据
builder.Services.Configure<ParkingLotDatabaseSettings>
    (builder.Configuration.GetSection("ParkingLotStoreDatabase")); //ParkingLotStoreDatabase is defined in appsettings.json

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //only open swagger under development enviroment
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { } //or add StartUp.cs