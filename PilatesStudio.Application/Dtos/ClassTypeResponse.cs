using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Dtos;

public record ClassTypeResponse(
    int Id,
    string Title,
    string? Description,
    int? Duration,
    int? Capacity,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static ClassTypeResponse FromClassType(ClassType classType) =>
        new(
            classType.Id,
            classType.Title,
            classType.Description,
            classType.Duration,
            classType.Capacity,
            classType.CreatedAt,
            classType.UpdatedAt);

    public static IEnumerable<ClassTypeResponse> FromClassTypes(IEnumerable<ClassType> classTypes) =>
        classTypes.Select(FromClassType);
}