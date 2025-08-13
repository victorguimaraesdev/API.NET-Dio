using API.NET.Domains.DTOs;
using API.NET.Entitys;

namespace API.NET.Domains.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> All(int page = 1, string? name = null, string? model = null);

        Vehicle? FindId(int id);

        void Save(Vehicle vehicle);

        void Update(Vehicle vehicle);

        void Delete(Vehicle vehicle);
    }
}