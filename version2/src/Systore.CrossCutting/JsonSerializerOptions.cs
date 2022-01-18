using System.Text.Json;
using System.Text.Json.Serialization;

namespace Systore.CrossCutting;

public static class StaticConfigurations
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        AllowTrailingCommas = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
}