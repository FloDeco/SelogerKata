using FluentAssertions;
using SeLoger.Api.dtos;
using SeLoger.Application.Tests.TestDoubles;
using SeLoger.Domain;

namespace SeLoger.Application.Tests;

public class ClassifiedAdCommandsTest
{
    [Fact]
    public void Create_ad_should_return_id_after_creating_it_and_status_should_be_waiting_for_validation()
    {
        //Arrange
        var classifiedAdRepositoryStub = new ClassifiedAdRepositoryDouble();
        var classifiedAdCommands       = new ClassifiedAdCommand(classifiedAdRepositoryStub);
        var classifiedAdCreationDto = new ClassifiedAdCreationDto
        {
            Title       = "fakeAd",
            Description = "nice house",
            Latitude    = 15.3,
            Longitude   = 16.1,
            Type        = 0
        };

        //Act
        var result = classifiedAdCommands.CreateAd(classifiedAdCreationDto);

        //Assert
        result.Should().Be(classifiedAdRepositoryStub.AdInserted.Id.ToString("D"));
        classifiedAdRepositoryStub.AdInserted.Status.Should().Be(ClassifiedAdStatus.WaitingForValidation);
    }

    [Fact]
    public void Should_set_status_to_published()
    {
        //Arrange
        var classifiedAdRepositoryDouble = new ClassifiedAdRepositoryDouble();
        var classifiedAdCommands         = new ClassifiedAdCommand(classifiedAdRepositoryDouble);
        var adId                         = Guid.NewGuid().ToString("D");

        //Act
        classifiedAdCommands.Validate(adId);

        //Assert
        classifiedAdRepositoryDouble.ClassifiedAdStatusChangedGuid.Should().Be(adId);
        classifiedAdRepositoryDouble.ClassifiedAdStatusChangedUsedStatus.Should().Be(ClassifiedAdStatus.Published);
    }
}