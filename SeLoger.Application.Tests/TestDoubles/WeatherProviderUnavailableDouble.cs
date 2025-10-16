using SeLoger.Application.Contracts;

namespace SeLoger.Application.Tests.TestDoubles;

internal class WeatherProviderUnavailableDouble : IWeatherProvider
{
    public Task<double> GetCelsiusTemperature(double latitude, double longitude)
    {
        throw new HttpRequestException("Weather API is unavailable");
    }
}
