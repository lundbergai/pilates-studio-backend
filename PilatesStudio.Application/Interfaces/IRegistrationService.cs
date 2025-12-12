namespace PilatesStudio.Application.Interfaces;

public interface IRegistrationService
{
    Task EnsureUserRegisteredAsync(string clerkId, string? name, string? email, IEnumerable<string> roles);
}