using WeatherApp.Models;

namespace WeatherApp.Services;

public interface ILocationService
{
    Task<LocationData>  GetCurrentLocation(string ip);
}