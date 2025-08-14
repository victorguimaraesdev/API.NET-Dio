using API.NET.Domains.DTOs;
using Microsoft.EntityFrameworkCore;
using API.NET.Domains.Interfaces;
using API.NET.Domains.Services;
using Microsoft.AspNetCore.Mvc;
using API.NET.Infrastructure.Db;
using API.NET.Domains.ModelViews;
using API.NET.Entitys;

#region Builder & BuilderServices
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<DataBase>();
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});
#endregion

#region App & Swagger
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home()));
#endregion

#region Login
app.MapPost("/administrador/login", ([FromBody] LoginDTO loginDTO, IAdministratorService administratorService) =>
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
#endregion

#region Vehicles

app.MapPost("/veiculos", ([FromBody] VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{
    var vehicle = new Vehicle
    {
        Name = vehicleDTO.Name,
        Model = vehicleDTO.Model,
        Year = vehicleDTO.Year
    };

    vehicleService.Save(vehicle);
    return Results.Created($"/veiculo/{vehicle.Id}", vehicle);

});

app.MapGet("/veiculos", ([FromQuery] int? page, IVehicleService vehicleService) =>
{
    var vehicles = vehicleService.All(page);

    return Results.Ok(vehicles);
});


#endregion


app.Run();
