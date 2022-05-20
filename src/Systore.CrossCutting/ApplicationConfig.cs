namespace Systore.CrossCutting;

public record ApplicationConfig
{
    public ConnectionStrings ConnectionStrings { get; init; }
    public ReleaseConfig ReleaseConfig { get; init; }
    public string Secret { get; init; }
}

public record ReleaseConfig : ApiConfig
{
    public string ClientId { get; init; }
}

public record ApiConfig
{
    public Uri BaseUrl { get; init; }
}