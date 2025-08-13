using API.NET.Domains.DTOs;
using Microsoft.EntityFrameworkCore;
using API.NET.Domains.Interfaces;
using API.NET.Domains.Services;
using Microsoft.AspNetCore.Mvc;
using API.NET.Infrastructure.Db;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<DataBase>();
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Hello World");


app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministratorService administratorService) =>
{
    if (administratorService.Login(loginDTO) != null)
    {
        return Results.Ok("Login realizado com sucesso");
    }
    else
    {
        return Results.Unauthorized();
    }
});


app.Run();
