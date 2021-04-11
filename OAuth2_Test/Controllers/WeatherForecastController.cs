using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OAuth2_Test.Models;
using OAuth2_Test.Services;

namespace OAuth2_Test.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Login()
        {
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return Ok();
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<WeatherForecast>> Get(
            [FromServices] IGoogleAuthProvider googleAuthProvider)
        {
            AuthenticateResult auth = await HttpContext.AuthenticateAsync();
            string idToken = (auth.Properties ?? throw new InvalidOperationException()).GetTokenValue(OpenIdConnectParameterNames.IdToken);
            _logger.LogDebug(idToken);
            if (User.Identity == null || !User.Identity.IsAuthenticated)
                return null;
            return await _weatherService.GetWeather();
        }
    }
}