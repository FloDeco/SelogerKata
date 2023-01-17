using Microsoft.EntityFrameworkCore;
using SeLoger.Application;
using SeLoger.Application.Contracts;
using SeLoger.Infrastructure.Persistence;
using SeLoger.Infrastructure.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
builder.Services.AddTransient<IClassifiedAdCommand, ClassifiedAdCommand>();
builder.Services.AddTransient<IClassifiedAdQueries, ClassifiedAdQueries>();
builder.Services.AddTransient<IWeatherProvider, WeatherProvider>();

builder.Services.AddDbContext<ClassifiedAdsDbContext>(optionsBuilder =>
                                                          optionsBuilder.UseInMemoryDatabase("ClassifiedAds"));

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