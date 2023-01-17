using SeLoger.Api.dtos;
using SeLoger.Application.Contracts;

namespace SeLoger.Api.Tests.TestDoubles;

public class ClassifiedAdCommandStub : IClassifiedAdCommand
{
    public string Id;
    public int    ValidateCalled;

    public string CreateAd(ClassifiedAdCreationDto creationDto)
    {
        Id = Guid.NewGuid().ToString("D");

        return Id;
    }

    public void Validate(string id)
    {
        ValidateCalled++;
    }
}