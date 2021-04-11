using System.Collections.Generic;
using System.Threading.Tasks;
using OAuth2_Test.Models;

namespace OAuth2_Test.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeather();
    }
}