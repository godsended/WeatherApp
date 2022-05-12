using System.Diagnostics;
using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService : IWeatherService
{
    private const string ApiKey = "eb98f11c11e64fe428ddeb4026a7631f";
    private readonly HttpClient httpClient;

    public WeatherService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<WeatherData> GetWeatherData(LocationData locationData)
    {
        string requestUrl =
            $"https://api.openweathermap.org/data/2.5/weather?lat={locationData.Latitude}&lon={locationData.Longitude}&appid={ApiKey}&units=metric";
        HttpContent content = new StringContent("");
        HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);
        string responseString = await response.Content.ReadAsStringAsync();

        Debug.WriteLine($@"Weather response string: {responseString}");
        WeatherData? result = JsonSerializer.Deserialize<WeatherData>(responseString);
        if (result == null)
        {
            result = new WeatherData();
            result.IsSuccess = false;
        }
        else
        {
            result.IsSuccess = true;
        }

        return result;
    }
}