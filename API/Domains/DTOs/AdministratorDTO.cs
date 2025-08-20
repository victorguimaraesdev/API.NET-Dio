using API.NET.Domains.Enuns;

namespace API.NET.Domains.DTOs;

public class AdministratorDTO
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Profile? Profile { get; set; } = default!;
}