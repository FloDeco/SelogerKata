using FluentAssertions;
using SeLoger.Application.Tests.TestDoubles;
using SeLoger.Domain;

namespace SeLoger.Application.Tests;

public class ClassifiedAdQueriesTest
{
    [Fact]
    public async Task Get_by_id_query_should_return_ad_information_when_ad_is_published()
    {
        //Arrange
        var repository            = new ClassifiedAdRepositoryDouble(ClassifiedAdStatus.Published);
        var weatherProviderDouble = new WeatherProviderDouble();
        var classifiedAdAdQueries = new ClassifiedAdQueries(repository, weatherProviderDouble);

        var id = Guid.NewGuid().ToString("D");

        //Act
        var classifiedAdDto = await classifiedAdAdQueries.GetByID(id);

        //Assert
        classifiedAdDto.Id.Should().Be(repository.GetByIdReturnedValue.Id.ToString("D"));
        classifiedAdDto.Title.Should().Be(repository.GetByIdReturnedValue.Title);
        classifiedAdDto.Status.Should().Be((int)repository.GetByIdReturnedValue.Status);
        classifiedAdDto.Type.Should().Be((int)repository.GetByIdReturnedValue.Type);
        classifiedAdDto.Description.Should().Be(repository.GetByIdReturnedValue.Description);
    }

    [Fact]
    public async Task
        Get_by_id_query_should_return_ad_information_with_weather_when_ad_is_published_and_weather_available()
    {
        //Arrange
        var repository            = new ClassifiedAdRepositoryDouble(ClassifiedAdStatus.Published);
        var weatherProviderDouble = new WeatherProviderDouble();
        var classifiedAdAdQueries = new ClassifiedAdQueries(repository, weatherProviderDouble);

        var id = Guid.NewGuid().ToString("D");

        //Act
        var classifiedAdDto = await classifiedAdAdQueries.GetByID(id);

        //Assert
        classifiedAdDto.Id.Should().Be(repository.GetByIdReturnedValue.Id.ToString("D"));
        classifiedAdDto.Title.Should().Be(repository.GetByIdReturnedValue.Title);
        classifiedAdDto.Status.Should().Be((int)repository.GetByIdReturnedValue.Status);
        classifiedAdDto.Type.Should().Be((int)repository.GetByIdReturnedValue.Type);
        classifiedAdDto.Description.Should().Be(repository.GetByIdReturnedValue.Description);
        classifiedAdDto.CelsiusTemperature.Should().Be(weatherProviderDouble.ReturnedTemperature);
    }

    [Fact]
    public async Task Get_by_id_query_should_return_empty_dto_when_id_is_null_or_status_not_published()
    {
        //Arrange
        var repositoryDouble      = new ClassifiedAdRepositoryDouble(ClassifiedAdStatus.WaitingForValidation);
        var weatherProviderDouble = new WeatherProviderDouble();
        var classifiedAdAdQueries = new ClassifiedAdQueries(repositoryDouble, weatherProviderDouble);
        var id                    = Guid.NewGuid().ToString("D");

        //Act
        var classifiedAdDto = await classifiedAdAdQueries.GetByID(id);

        //Assert
        classifiedAdDto.Id.Should().BeNullOrEmpty();
    }
}