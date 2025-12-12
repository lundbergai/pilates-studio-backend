using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository repository) : ControllerBase
{
    private readonly IUserRepository _repository = repository;
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _repository.GetAllAsync();
        
        return Ok(UserDto.FromUsers(users));
    }
    
    [HttpGet("instructors")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
    {
        var instructors = await _repository.GetInstructorsByRoleAsync();
    
        return Ok(InstructorDto.FromUsers(instructors));
    }
}