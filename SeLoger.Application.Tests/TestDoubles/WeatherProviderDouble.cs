using SeLoger.Application.Contracts;

namespace SeLoger.Application.Tests.TestDoubles;

internal class WeatherProviderDouble : IWeatherProvider
{
    public double ReturnedTemperature;

    public Task<double> GetCelsiusTemperature(double latitude, double longitude)
    {
        ReturnedTemperature = 15.0;
        return Task.FromResult(ReturnedTemperature);
    }
}