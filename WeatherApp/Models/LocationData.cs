using System.Text.Json.Serialization;

namespace WeatherApp.Models;

[Serializable]
public class LocationData
{
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }
    [JsonPropertyName("lon")]
    public float Longitude { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    public bool IsSuccess { get; set; }
}