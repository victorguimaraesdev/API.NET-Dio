namespace API.NET.Domains.ModelViews;

public record AdmLogado
{
    public string Email { get; set; } = default!;
    public string Profile { get; set; } = default!;
    public string Token { get; set; } = default!;
}