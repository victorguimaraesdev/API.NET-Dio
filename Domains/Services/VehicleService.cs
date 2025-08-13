using API.NET.Domains.Interfaces;
using API.NET.Entitys;
using API.NET.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace API.NET.Domains.Services;

public class VehicleService : IVehicleService
{
    private readonly DataBase _db;

    public VehicleService(DataBase db)
    {
        _db = db;
    }

    public List<Vehicle> All(int page = 1, string? name = null, string? model = null)
    {
        var query = _db.Vehicles.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(v => EF.Functions.Like(v.Name.ToLower(), $"%{name}%"));
        }

        int itensForPage = 10;

        query = query.Skip((page - 1) * itensForPage).Take(itensForPage);

        return query.ToList();
    }


    public Vehicle? FindId(int id)
    {
        return _db.Vehicles.Where(v => v.Id == id).FirstOrDefault();
    }

    public void Save(Vehicle vehicle)
    {
        _db.Vehicles.Add(vehicle);
        _db.SaveChanges();
    }

    public void Update(Vehicle vehicle)
    {
        _db.Vehicles.Update(vehicle);
        _db.SaveChanges();
    }
    public void Delete(Vehicle vehicle)
    {
        _db.Vehicles.Remove(vehicle);
        _db.SaveChanges();
    }
}