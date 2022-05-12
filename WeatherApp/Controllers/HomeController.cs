using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherService weatherService;
    private readonly ILocationService locationService;

    public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, 
        ILocationService locationService)
    {
        _logger = logger;
        this.weatherService = weatherService;
        this.locationService = locationService;
    }

    public IActionResult Index()
    {
        MainIndexViewModel viewModel = GetMainIndexViewModel().Result;
        Debug.WriteLine($@"Weather object result: {JsonSerializer.Serialize(viewModel)}");
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public async Task<MainIndexViewModel> GetMainIndexViewModel()
    {
        MainIndexViewModel result = new MainIndexViewModel();
        
        IPAddress? ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
        if (ipAddress == null)
        {
            throw new NullReferenceException("Remote IP Adress is NULL");
        }

        string ip = ipAddress.ToString();
        #if DEBUG
        ip = "79.170.109.211";
        #endif
        LocationData locationData = await locationService.GetCurrentLocation(ip);
        result.Location = locationData;

        WeatherData weatherData;
            
        if (!locationData.IsSuccess)
        {
            weatherData = new WeatherData();
            weatherData.IsSuccess = false;
            result.Weather = weatherData;
            return result;
        }

        weatherData = await weatherService.GetWeatherData(locationData);

        result.Weather = weatherData;

        return result;
    }
}