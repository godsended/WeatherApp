using System.Diagnostics;
using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class LocationService : ILocationService
{
    private readonly HttpClient httpClient;

    public LocationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<LocationData> GetCurrentLocation(string ip)
    {
        string request = $@"http://ip-api.com/json/{ip}?fields=61439";
        StringContent content = new StringContent("");

        HttpResponseMessage responseMessage = await httpClient.PostAsync(request, content);
        string responseString = await responseMessage.Content.ReadAsStringAsync();
        
        Debug.WriteLine($@"Location response string: {responseString}");
        
        LocationData? result =
            JsonSerializer.Deserialize<LocationData>(responseString);
        
        if (result == null)
        {
            result = new LocationData();
            result.IsSuccess = true;
        }
        else
        {
            result.IsSuccess = true;
        }
        return result;
    }
}