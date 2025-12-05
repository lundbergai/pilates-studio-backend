using Microsoft.AspNetCore.Mvc;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var hello = "Hello World";
        
        return Ok(hello);
    }
}