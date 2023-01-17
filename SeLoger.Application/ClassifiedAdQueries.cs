using SeLoger.Application.Contracts;
using SeLoger.Application.Dtos;
using SeLoger.Domain;

namespace SeLoger.Application;

public class ClassifiedAdQueries : IClassifiedAdQueries
{
    private readonly IClassifiedAdRepository classifiedAdRepository;
    private readonly IWeatherProvider        weatherProvider;

    public ClassifiedAdQueries(IClassifiedAdRepository classifiedAdRepository, IWeatherProvider weatherProvider)
    {
        this.classifiedAdRepository = classifiedAdRepository;
        this.weatherProvider        = weatherProvider;
    }

    public async Task<ClassifiedAdDto> GetByID(string id)
    {
        var classifiedAd = classifiedAdRepository.GetById(Guid.Parse(id));

        if (classifiedAd == null || classifiedAd.Status != ClassifiedAdStatus.Published)
            return new ClassifiedAdDto();

        var celsiusTemperature = await weatherProvider
                                       .GetCelsiusTemperature(classifiedAd.Latitude, classifiedAd.Longitude)
                                       .ConfigureAwait(false);

        return new ClassifiedAdDto
        {
            Id                 = classifiedAd.Id.ToString("D"),
            Title              = classifiedAd.Title,
            Description        = classifiedAd.Description,
            Latitude           = classifiedAd.Latitude,
            Longitude          = classifiedAd.Longitude,
            Status             = (int)classifiedAd.Status,
            Type               = (int)classifiedAd.Type,
            CelsiusTemperature = celsiusTemperature
        };
    }
}