using FluentAssertions;
using Flurl.Http.Testing;
using SeLoger.Infrastructure.Weather;

namespace SeLoger.Infrastructure.Tests;

public class WeatherProviderTest
{
    [Fact]
    public void Weather_provider_should_get_teperature_from_api()
    {
        //Arrange
        var provider  = new WeatherProvider();
        var weather   = new Weather.Weather();
        var latitude  = 15.2;
        var longitude = 14.2;
        weather.Current_weather = new CurrentWeather { Temperature = 6 };

        using (var httpTest = new HttpTest())
        {
            httpTest.ForCallsTo("https://api.open-meteo.com/v1/forecast").WithQueryParams(new
            {
                latitude, longitude, current_weather = true
            }).RespondWithJson(weather);


            //Act
            var celsiusTemperature = provider.GetCelsiusTemperature(latitude, longitude).Result;


            celsiusTemperature.Should().Be(weather.Current_weather.Temperature);
        }
    }
}