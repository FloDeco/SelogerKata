using SeLoger.Api.dtos;
using SeLoger.Application.Contracts;
using SeLoger.Domain;

namespace SeLoger.Application;

public class ClassifiedAdCommand : IClassifiedAdCommand
{
    private readonly IClassifiedAdRepository classifiedAdRepository;

    public ClassifiedAdCommand(IClassifiedAdRepository classifiedAdRepository)
    {
        this.classifiedAdRepository = classifiedAdRepository;
    }

    public string CreateAd(ClassifiedAdCreationDto creationDto)
    {
        var adCreationRequest = new ClassifiedAd
        {
            Title       = creationDto.Title,
            Description = creationDto.Description,
            Latitude    = creationDto.Latitude,
            Longitude   = creationDto.Longitude,
            Type        = (ClassifiedAdType)creationDto.Type
        };

        var classifiedAd = classifiedAdRepository.Insert(adCreationRequest);

        return classifiedAd.Id.ToString("D");
    }

    public void Validate(string id)
    {
        classifiedAdRepository.ChangeAdStatus(Guid.Parse(id), ClassifiedAdStatus.Published);
    }
}