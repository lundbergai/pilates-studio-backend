using Microsoft.EntityFrameworkCore;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;
using PilatesStudio.Infrastructure.Persistence;

namespace PilatesStudio.Infrastructure.Repositories;

public class BookingRepository(PilatesDbContext context) : IBookingRepository
{
    private readonly PilatesDbContext _context = context;
    
    public async Task<List<Booking>> GetUserBookingsAsync(int userId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.ScheduledClass)
            .ThenInclude(sc => sc.ClassType)
            .Include(b => b.ScheduledClass.Instructor)
            .OrderBy(b => b.ScheduledClass.StartTime)
            .ToListAsync();
    }
    
    public async Task<Booking?> GetByIdAsync(int bookingId)
    {
        return await _context.Bookings
            .Include(b => b.ScheduledClass)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
    }
    
    public async Task<Booking?> CreateAsync(int userId, CreateBookingDto dto)
    {
        var booking = new Booking
        {
            UserId = userId,
            ScheduledClassId = dto.ScheduledClassId,
            BookedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Bookings.Add(booking);
        
        return booking;
    }

    public async Task<bool> DeleteAsync(int bookingId, int userId)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);
        if (booking == null)
            return false;

        _context.Bookings.Remove(booking);
        return true;
    }
}