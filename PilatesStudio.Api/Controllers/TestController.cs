using Microsoft.AspNetCore.Mvc;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Infrastructure.Repositories;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(DemoRepository repository) : ControllerBase
{
    private readonly DemoRepository _repository = repository;
    
    [HttpGet]
    public ActionResult<string> GetHelloWorld()
    {
        var hello = "Hello World";
        
        return Ok(hello);
    }
    
    [HttpGet]
    [Route("ConnectionString")]
    public ActionResult<string> GetConnectionString()
    {
        var builder = WebApplication.CreateBuilder();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        if (connectionString == null)
            return NotFound();
        
        return Ok(connectionString);
    }

    [HttpGet("{name}")] 
    public async Task<ActionResult<DemoResponseDto>> GetDemo(string name)
    {
        var demo = await _repository.GetDemoAsync(name);

        if (demo == null)
            return NotFound();
        
        return Ok(new DemoResponseDto(demo.Id, demo.Name));
    }

    [HttpPost]
    public async Task<ActionResult<DemoResponseDto>> CreateDemo(DemoRequestDto request)
    {
        var demo = await _repository.AddDemoAsync(request.Name);
        
        return CreatedAtAction(
            nameof(GetDemo),
            new { demo.Name },
            new DemoResponseDto(demo.Id, demo.Name));
    }
}