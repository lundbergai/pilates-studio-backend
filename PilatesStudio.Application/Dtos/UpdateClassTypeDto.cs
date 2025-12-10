namespace PilatesStudio.Application.Dtos;

public record UpdateClassTypeDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int? Duration { get; init; }
    public int? Capacity { get; init; }
}