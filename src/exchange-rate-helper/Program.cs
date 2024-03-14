using Application.Services;
using Application.Services.Gateways;
using Data.PostGreSql;
using Data.PostGreSql.Repositories;
using Domain.Core.Repositories;
using Infrastructure.CrossCutting.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<ExceptionsHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ICurrencyExchangeRateGateway, CurrencyExchangeRateGateway>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton);

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.Run();