using Microsoft.AspNetCore.Mvc;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassTypesController(IClassTypesRepository repository) : ControllerBase
{
    private readonly IClassTypesRepository _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassTypeResponse>>> GetAll()
    {
        var classTypes = await _repository.GetClassTypesAsync();
        
        return Ok(ClassTypeResponse.FromClassTypes(classTypes));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassTypeResponse>> GetById(int id)
    {
        var classType = await _repository.GetClassTypeAsync(id);
        if (classType == null)
            return NotFound();

        return Ok(ClassTypeResponse.FromClassType(classType));
    }

    [HttpPost]
    public async Task<ActionResult<ClassTypeResponse>> Create(CreateClassTypeDto dto)
    {
        var classType = await _repository.CreateClassTypeAsync(dto);
        var response = ClassTypeResponse.FromClassType(classType);
        
        return CreatedAtAction(nameof(GetById), new { id = classType.Id }, response);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ClassTypeResponse>> Update(int id, UpdateClassTypeDto dto)
    {
        if (!dto.HasChanges())
            return BadRequest("Provide one or more changes to update");

        var classType = await _repository.UpdateClassTypeAsync(id, dto);
        if (classType == null)
            return NotFound();

        return Ok(ClassTypeResponse.FromClassType(classType));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteClassTypeAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}