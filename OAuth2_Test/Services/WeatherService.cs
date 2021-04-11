using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OAuth2_Test.Models;

namespace OAuth2_Test.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        public async Task<IEnumerable<WeatherForecast>> GetWeather()
        {
            return await Task.Run(() =>
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                    .ToArray();
            });
            
        }
    }
}