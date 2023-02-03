using APlaceToPrrLong;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);
//Get connection string
string connectionString = builder.Configuration.GetConnectionString("DevConnection");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configure Connection String
builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

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
