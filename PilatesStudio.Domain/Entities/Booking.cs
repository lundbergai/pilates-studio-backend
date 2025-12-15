namespace PilatesStudio.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime BookedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ScheduledClassId { get; set; }
    public ScheduledClass ScheduledClass { get; set; } = null!;
}