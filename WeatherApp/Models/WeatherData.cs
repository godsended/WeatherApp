using System.Text.Json.Serialization;

namespace WeatherApp.Models;

[Serializable]
public class WeatherData
{
    [JsonPropertyName("main")]
    public WeatherMainInfo? Main { get; set; }
    [JsonPropertyName("weather")]
    public WeatherTypeInfo[]? Weather { get; set; }
    public bool IsSuccess { get; set; }

    [Serializable]
    public class WeatherMainInfo
    {
        [JsonPropertyName("temp")]
        public float Temp { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }
    
    [Serializable]
    public class WeatherTypeInfo
    {
        [JsonPropertyName("main")]
        public string? Main { get; set; }
    }
}