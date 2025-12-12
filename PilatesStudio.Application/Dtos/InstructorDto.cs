using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Dtos;

public class InstructorDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public static IEnumerable<InstructorDto> FromUsers(IEnumerable<User> users)
    {
        return users.Select(u => new InstructorDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email
        });
    }
}