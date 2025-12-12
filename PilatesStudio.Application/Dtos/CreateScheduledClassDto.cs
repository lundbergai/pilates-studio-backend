namespace PilatesStudio.Application.Dtos;

public class CreateScheduledClassDto
{
    public int ClassTypeId { get; set; }
    public int InstructorId { get; set; }
    public string StartTime { get; set; } = string.Empty;
}