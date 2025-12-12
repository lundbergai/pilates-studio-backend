using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ScheduledClassDto>>> GetAll()
    {
        var classes = await _repository.GetAllAsync();

        return Ok(ScheduledClassDto.FromScheduledClasses(classes));
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ScheduledClassDto>> Create([FromBody] CreateScheduledClassDto dto)
    {
        var scheduledClass = await _repository.CreateAsync(dto);
        var response = ScheduledClassDto.FromScheduledClass(scheduledClass);
        
        return CreatedAtAction(nameof(GetAll), new { id = scheduledClass.Id }, response);
    }
    
    [HttpPatch("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ScheduledClassDto>> Update(int id, [FromBody] UpdateScheduledClassDto dto)
    {
        var scheduledClass = await _repository.UpdateAsync(id, dto);
        
        if (scheduledClass == null)
            return NotFound();
        
        return Ok(ScheduledClassDto.FromScheduledClass(scheduledClass));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _repository.DeleteAsync(id);
        
        if (!success)
            return NotFound();
        
        return NoContent();
    }
}