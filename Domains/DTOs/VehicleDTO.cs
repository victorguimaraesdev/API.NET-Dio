namespace API.NET.Domains.DTOs;

public record VehicleDTO
{
    public string Name { get; set; } = default!;

    public string Model { get; set; } = default!;

    public int Year { get; set; } = default!;
}