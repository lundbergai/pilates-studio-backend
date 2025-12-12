using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Dtos;

public record UserDto(
    int Id,
    string FullName,
    string Email,
    string? ClerkUserId,
    bool IsAdmin,
    bool IsInstructor,
    bool IsMember,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static UserDto FromUser(User user) =>
        new(
            user.Id,
            user.FullName,
            user.Email,
            user.ClerkUserId,
            user.IsAdmin,
            user.IsInstructor,
            user.IsMember,
            user.CreatedAt,
            user.UpdatedAt
        );
    
    public static IEnumerable<UserDto> FromUsers(IEnumerable<User> users) => 
        users.Select(FromUser);
}