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
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await repository.GetAllAsync();
        
        return Ok(UserDto.FromUsers(users));
    }
}