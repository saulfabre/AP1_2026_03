using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Data;
using RegistroPrestamos.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<LibrosServices>();

builder.Services.AddDbContextFactory<RegistroPrestamosDbContext>(o =>
 o.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlConStr")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

