namespace PilatesStudio.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? ClerkUserId { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsInstructor { get; set; }
    public bool IsMember { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}