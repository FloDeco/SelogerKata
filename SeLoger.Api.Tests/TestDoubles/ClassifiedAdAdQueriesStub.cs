using SeLoger.Application.Contracts;
using SeLoger.Application.Dtos;

namespace SeLoger.Api.Tests.TestDoubles;

public class ClassifiedAdAdQueriesStub : IClassifiedAdQueries
{
    public ClassifiedAdDto ClassifiedAdDto { get; set; }

    public Task<ClassifiedAdDto> GetByID(string id)
    {
        ClassifiedAdDto = new ClassifiedAdDto
        {
            Id          = id,
            Title       = "House 3",
            Description = "nice house with huge kitchen",
            Latitude    = 1.2,
            Longitude   = 13.2,
            Status      = 1,
            Type        = 1
        };
        return Task.FromResult(ClassifiedAdDto);
    }
}