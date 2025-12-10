using System.ComponentModel.DataAnnotations;

namespace PilatesStudio.Application.Dtos;

public record CreateClassTypeDto
{
    [Required] 
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public int? Duration { get; init; }
    public int? Capacity { get; init; }
}