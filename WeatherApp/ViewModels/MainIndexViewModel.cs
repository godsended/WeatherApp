using WeatherApp.Models;

namespace WeatherApp.ViewModels;

[Serializable]
public class MainIndexViewModel
{
    public LocationData? Location { get; set; }
    public WeatherData? Weather { get; set; }
}