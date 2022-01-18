using System.Net.Http.Json;
using System.Text.Json;
using Systore.CrossCutting;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;

namespace Systore.Repositories;

public class ReleaseRepository : IReleaseRepository
{
    private readonly HttpClient _httpClient;

    public ReleaseRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> CheckHealth()
    {
        try
        {
            using var response = await _httpClient.GetAsync("/api/v1/health-check/ping");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task<bool> VerifyRelease(string clientId)
    {
        var request = new {clientId = clientId};
        using var jsonContent = JsonContent.Create(request, options: StaticConfigurations.JsonSerializerOptions);
        using var httpResponse = await _httpClient.PostAsync("/checkRelease", jsonContent);
        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        var releaseResponse = JsonSerializer.Deserialize<ReleaseResponse>(responseBody, StaticConfigurations.JsonSerializerOptions)!;
        return releaseResponse.Release;
    }
}