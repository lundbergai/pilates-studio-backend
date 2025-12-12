namespace PilatesStudio.Domain.Entities;

public class ScheduledClass
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public int BookedSpots { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public int ClassTypeId { get; set; }
    public ClassType ClassType { get; set; } = null!;
}