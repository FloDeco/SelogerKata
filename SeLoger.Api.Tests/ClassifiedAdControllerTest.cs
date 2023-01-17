using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SeLoger.Api.Controllers;
using SeLoger.Api.dtos;
using SeLoger.Api.Tests.TestDoubles;

namespace SeLoger.Api.Tests;

public class ClassifiedAdControllerTest
{
    [Fact]
    public void Post_ad_should_return_id_of_one_ad()
    {
        //Arrange

        var creationDto = new ClassifiedAdCreationDto
        {
            Title       = "bel appartement",
            Description = "spacieux; 25 m²",
            Longitude   = 15.1,
            Latitude    = 16.1,
            Type        = 1
        };

        var commandsDouble = new ClassifiedAdCommandStub();
        var querieStub     = new ClassifiedAdAdQueriesStub();

        var classifiedAdController = new ClassifiedAdController(commandsDouble, querieStub);

        //Act
        var result = classifiedAdController.Post(creationDto);

        //Assert
        result.Should().Be(commandsDouble.Id);
    }

    [Fact]
    public void Validate_ad_should_return_204()
    {
        //Arrange
        var id = Guid.NewGuid().ToString("D");

        var commandsDouble         = new ClassifiedAdCommandStub();
        var querieStub             = new ClassifiedAdAdQueriesStub();
        var classifiedAdController = new ClassifiedAdController(commandsDouble, querieStub);

        //Act
        var result = classifiedAdController.Validate(id);

        //Assert
        commandsDouble.ValidateCalled.Should().Be(1);
        var noContentResult = result as NoContentResult;
        noContentResult.StatusCode.Should().Be(new NoContentResult().StatusCode);
    }

    [Fact]
    public async Task Get_ad_should_return_a_full_classified_ad()
    {
        //Arrange
        var id = Guid.NewGuid().ToString("D");

        var commandsDouble         = new ClassifiedAdCommandStub();
        var querieStub             = new ClassifiedAdAdQueriesStub();
        var classifiedAdController = new ClassifiedAdController(commandsDouble, querieStub);

        //Act
        var result = await classifiedAdController.Get(id);

        //Assert
        result.Value.Id.Should().Be(querieStub.ClassifiedAdDto.Id);
        result.Value.Description.Should().Be(querieStub.ClassifiedAdDto.Description);
        result.Value.Status.Should().Be(querieStub.ClassifiedAdDto.Status);
        result.Value.Title.Should().Be(querieStub.ClassifiedAdDto.Title);
        result.Value.Latitude.Should().Be(querieStub.ClassifiedAdDto.Latitude);
        result.Value.Longitude.Should().Be(querieStub.ClassifiedAdDto.Longitude);
        result.Value.CelsiusTemperature.Should().Be(querieStub.ClassifiedAdDto.CelsiusTemperature);
        result.Value.Type.Should().Be(querieStub.ClassifiedAdDto.Type);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Get_ad_should_return_401_not_found_when_id_is_null_or_empty(string id)
    {
        //Arrange
        var commandsDouble         = new ClassifiedAdCommandStub();
        var querieStub             = new ClassifiedAdAdQueriesStub();
        var classifiedAdController = new ClassifiedAdController(commandsDouble, querieStub);

        //Act
        var result = await classifiedAdController.Get(id);

        //Assert
        var notFoundResult = result.Result as NotFoundResult;
        notFoundResult.StatusCode.Should().Be(new NotFoundResult().StatusCode);
    }
}