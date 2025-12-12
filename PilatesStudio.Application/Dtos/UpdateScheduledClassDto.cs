namespace PilatesStudio.Application.Dtos;

public class UpdateScheduledClassDto
{
    public int? ClassTypeId { get; set; }
    public int? InstructorId { get; set; }
    public string? StartTime { get; set; }
}