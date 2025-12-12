using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Dtos;

public record ScheduledClassDto(
    int Id,
    int ClassTypeId,
    string ClassTypeName,
    int ClassTypeDuration,
    int ClassTypeCapacity,
    DateTime StartTime,
    int BookedSpots,
    string? Instructor,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static ScheduledClassDto FromScheduledClass(ScheduledClass sc) =>
        new(
            sc.Id,
            sc.ClassTypeId,
            sc.ClassType.Title,
            sc.ClassType.Duration ?? 0,
            sc.ClassType.Capacity ?? 0,
            sc.StartTime,
            sc.BookedSpots,
            null,
            sc.CreatedAt,
            sc.UpdatedAt
        );
    
    public static IEnumerable<ScheduledClassDto> FromScheduledClasses(IEnumerable<ScheduledClass> classes) =>
        classes.Select(FromScheduledClass);
}