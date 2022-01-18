namespace Systore.Api;

/// <summary>
/// WeatherForecast class
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Date property
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// TemperatureC property
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// TemperatureF 
    /// </summary>
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

    /// <summary>
    /// Summary property
    /// </summary>
    public string? Summary { get; set; }
}