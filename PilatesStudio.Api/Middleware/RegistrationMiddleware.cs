using System.Security.Claims;
using PilatesStudio.Application.Interfaces;

namespace PilatesStudio.Api.Middleware;

public class RegistrationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    
    public async Task InvokeAsync(HttpContext context, IRegistrationService service)
    {
        
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var clerkId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = context.User.FindFirst("full_name")?.Value;
            var email = context.User.FindFirst("primary_email")?.Value;

            if (!string.IsNullOrEmpty(clerkId))
            {
                var roles = context.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value);
                
                await service.EnsureUserRegisteredAsync(clerkId, name, email, roles);
            }
        }
        
        await _next(context);
    }
}