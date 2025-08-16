using API.NET.Domains.DTOs;
using Microsoft.EntityFrameworkCore;
using API.NET.Domains.Interfaces;
using API.NET.Domains.Services;
using Microsoft.AspNetCore.Mvc;
using API.NET.Infrastructure.Db;
using API.NET.Domains.ModelViews;
using API.NET.Entitys;
using API.NET.Domains.Enuns;


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
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region AdiministratorLogin


app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministratorService administratorService) =>
{
    if (administratorService.Login(loginDTO) != null)
    {
        return Results.Ok("Login realizado com sucesso");
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Administrators");

app.MapPost("/administradores/", ([FromBody] AdministratorDTO administratorDTO, IAdministratorService administratorService) =>
{
    var validationError = new ValidationError
    {
        Messages = new List<string>()
    };

    if (string.IsNullOrEmpty(administratorDTO.Email))
    {
        validationError.Messages.Add("O E-mail não pode ser vazio");
    }

    if (string.IsNullOrEmpty(administratorDTO.Password))
    {
        validationError.Messages.Add("A senha não pode ser vazia");
    }

    if (administratorDTO.Profile == null)
    {
        validationError.Messages.Add("O perfil não pode ser vazio");
    }

    if (validationError.Messages.Count > 0)
    {
        return Results.BadRequest(validationError);
    }

    var administrator = new Administrator
    {
        Email = administratorDTO.Email,
        Password = administratorDTO.Password,
        Profile = administratorDTO.Profile.ToString() ?? Profile.Editor.ToString()
    };

    administratorService.Save(administrator);
    return Results.Created($"/administrador/{administrator.Id}", administrator);

}).WithTags("Administrators");

app.MapGet("/administradores/", ([FromQuery] int? page, IAdministratorService administratorService) =>
{
    var adms = new List<AdministratorModelView>();
    var administradors = administratorService.All(page);
    foreach (var adm in administradors)
    {
        adms.Add(new AdministratorModelView
        {
            Id = adm.Id,
            Email = adm.Email,
            Profile = (Profile)Enum.Parse(typeof(Profile), adm.Profile)
        });
    }
    return Results.Ok(adms);

}).WithTags("Administrators");

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministratorService administratorService) =>
{
    var administrator = administratorService.Find(id);
    if (administrator == null) return Results.NotFound();

    return Results.Ok(administrator);

}).WithTags("Administrators"); 

#endregion

#region ValidationError
ValidationError validDTO(VehicleDTO vehicleDTO)
{
    var validationError = new ValidationError
    {
        Messages = new List<string>()
    };

    if (string.IsNullOrEmpty(vehicleDTO.Name))
    {
        validationError.Messages.Add("O nome não pode ser vazio.");
    }
    if (string.IsNullOrEmpty(vehicleDTO.Model))
    {
        validationError.Messages.Add("O modelo não pode ser vazio.");
    }
    if (vehicleDTO.Year < 1950)
    {
        validationError.Messages.Add("Ano do veiculo invalido ou vazio.");
    }
    return validationError;
}
#endregion

#region Vehicles


app.MapPost("/veiculos", ([FromBody] VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{

    var validationError = validDTO(vehicleDTO);
    if (validationError.Messages.Count > 0)
    {
        return Results.BadRequest(validationError);
    }

    var vehicle = new Vehicle
    {
        Name = vehicleDTO.Name,
        Model = vehicleDTO.Model,
        Year = vehicleDTO.Year
    };

    vehicleService.Save(vehicle);
    return Results.Created($"/veiculo/{vehicle.Id}", vehicle);

}).WithTags("Vehicles");

app.MapGet("/veiculos", ([FromQuery] int? page, IVehicleService vehicleService) =>
{
    var vehicles = vehicleService.All(page);

    return Results.Ok(vehicles);

}).WithTags("Vehicles");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindId(id);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado");

    return Results.Ok(vehicle);
    
}).WithTags("Vehicles");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindId(id);
    if (vehicle == null) return Results.NotFound("Veículo não encontrado");

    var validationError = validDTO(vehicleDTO);
    if (validationError.Messages.Count > 0)
    {
        return Results.BadRequest(validationError);
    }

    vehicle.Name = vehicleDTO.Name;
    vehicle.Model = vehicleDTO.Model;
    vehicle.Year = vehicleDTO.Year;

    vehicleService.Update(vehicle);

    return Results.Ok(vehicle);

}).WithTags("Vehicles");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindId(id);
    if (vehicle == null) return Results.NotFound("Veiculo não encontrado");

    vehicleService.Delete(vehicle);

    return Results.NoContent();

}).WithTags("Vehicles");

#endregion


app.Run();
