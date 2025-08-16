using API.NET.Domains.Enuns;

namespace API.NET.Domains.ModelViews;

public record AdministratorModelView
{
    public int Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Profile Profile { get; set; } = default!;
}