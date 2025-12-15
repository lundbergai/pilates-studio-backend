using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Dtos;

public class BookingDto
{
    public int Id { get; set; }
    public DateTime BookedAt { get; set; }
    public int UserId { get; set; }
    public int ScheduledClassId { get; set; }
    public ScheduledClassDto? ScheduledClass { get; set; }

    public static BookingDto FromBooking(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            BookedAt = booking.BookedAt,
            UserId = booking.UserId,
            ScheduledClassId = booking.ScheduledClassId,
            ScheduledClass = booking.ScheduledClass != null ? ScheduledClassDto.FromScheduledClass(booking.ScheduledClass) : null
        };
    }
}