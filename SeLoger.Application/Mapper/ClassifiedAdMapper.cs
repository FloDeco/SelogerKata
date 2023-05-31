using SeLoger.Application.Dtos;
using SeLoger.Domain;

namespace SeLoger.Application.Mapper;

internal static class ClassifiedAdMapper
{
    public static ClassifiedAdDto ToDto(ClassifiedAd classifiedAd, double celsiusTemperature)
    {
        return new ClassifiedAdDto
        {
            Id = classifiedAd.Id.ToString("D"),
            Title = classifiedAd.Title,
            Description = classifiedAd.Description,
            Latitude = classifiedAd.Latitude,
            Longitude = classifiedAd.Longitude,
            Status = (int) classifiedAd.Status,
            Type = (int) classifiedAd.Type,
            CelsiusTemperature = celsiusTemperature
        };
    }
}