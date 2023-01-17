using SeLoger.Api.dtos;

namespace SeLoger.Application.Contracts;

public interface IClassifiedAdCommand
{
    string CreateAd(ClassifiedAdCreationDto creationDto);
    void   Validate(string                  id);
}