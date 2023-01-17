using SeLoger.Application.Dtos;

namespace SeLoger.Application.Contracts;

public interface IClassifiedAdQueries
{
    Task<ClassifiedAdDto> GetByID(string id);
}