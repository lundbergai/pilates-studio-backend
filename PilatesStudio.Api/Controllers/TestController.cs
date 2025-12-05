using Microsoft.AspNetCore.Mvc;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
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
}