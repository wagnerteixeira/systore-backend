namespace Systore.CrossCutting.Models;

public record User
{
    public int Id { get; init; }
    public string UserName { get; init; } = String.Empty;
    public string Password { get; init; } = String.Empty;
    public bool Admin { get; set; }
}