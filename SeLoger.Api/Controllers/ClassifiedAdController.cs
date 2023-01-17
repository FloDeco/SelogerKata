using Microsoft.AspNetCore.Mvc;
using SeLoger.Api.dtos;
using SeLoger.Application.Contracts;
using SeLoger.Application.Dtos;

namespace SeLoger.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassifiedAdController : ControllerBase
{
    private readonly IClassifiedAdCommand adCommand;
    private readonly IClassifiedAdQueries classifiedAdQueries;

    public ClassifiedAdController(IClassifiedAdCommand adCommand, IClassifiedAdQueries classifiedAdQueries)
    {
        this.adCommand           = adCommand;
        this.classifiedAdQueries = classifiedAdQueries;
    }

    [HttpPost]
    public string Post(ClassifiedAdCreationDto creationDto)
    {
        return adCommand.CreateAd(creationDto);
    }

    [HttpPatch]
    public ActionResult Validate(string id)
    {
        adCommand.Validate(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<ClassifiedAdDto>> Get(string id)
    {
        var classifiedAdDto = await classifiedAdQueries.GetByID(id).ConfigureAwait(false);

        if (string.IsNullOrEmpty(classifiedAdDto.Id))
            return NotFound();

        return classifiedAdDto;
    }
}