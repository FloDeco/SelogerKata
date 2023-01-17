namespace SeLoger.Application.Contracts;

public interface IWeatherProvider
{
    Task<double> GetCelsiusTemperature(double latitude, double longitude);
}