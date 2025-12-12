using Microsoft.AspNetCore.Mvc;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduledClassesController(IScheduledClassRepository repository) : ControllerBase
{
    private readonly IScheduledClassRepository _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduledClassDto>>> GetAll()
    {
        var classes = await _repository.GetAllAsync();

        return Ok(ScheduledClassDto.FromScheduledClasses(classes));
    }
}