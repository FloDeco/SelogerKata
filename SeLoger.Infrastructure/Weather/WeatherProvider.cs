using Flurl;
using Flurl.Http;
using SeLoger.Application.Contracts;

namespace SeLoger.Infrastructure.Weather;

public class WeatherProvider : IWeatherProvider
{
    public async Task<double> GetCelsiusTemperature(double latitude, double longitude)
    {
        var weather = await "https://api.open-meteo.com/v1/"
                            .AppendPathSegment("forecast")
                            .SetQueryParams(new { latitude, longitude, current_weather = true })
                            .GetJsonAsync<Weather>()
                            .ConfigureAwait(false);

        return weather.Current_weather.Temperature;
    }
}