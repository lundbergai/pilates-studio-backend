using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Services;

public class RegistrationService(IUserRepository repository) : IRegistrationService
{
    private readonly IUserRepository _repository = repository;

    public async Task EnsureUserRegisteredAsync(string clerkId, string? name, string? email, IEnumerable<string> roles)
    {
        var existingUser = await _repository.GetByClerkIdAsync(clerkId);

        if (existingUser == null)
        {
            var user = new User
            {
                FullName = name ?? string.Empty,
                Email = email ?? string.Empty,
                ClerkUserId = clerkId,
                IsAdmin = roles.Contains("admin"),
                IsInstructor = roles.Contains("instructor"),
                IsMember = roles.Contains("member"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            await _repository.AddAsync(user);
        }
    }
}