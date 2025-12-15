using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IScheduledClassRepository _scheduledClassRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        IScheduledClassRepository scheduledClassRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _scheduledClassRepository = scheduledClassRepository;
    }
    
    public async Task<List<Booking>> GetUserBookingsAsync(string? clerkUserId)
    {
        if (string.IsNullOrEmpty(clerkUserId))
            throw new InvalidOperationException("Clerk user ID is missing.");

        var user = await _userRepository.GetByClerkIdAsync(clerkUserId);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        return await _bookingRepository.GetUserBookingsAsync(user.Id);
    }

    public async Task<Booking?> CreateBookingAsync(string? clerkUserId, CreateBookingDto dto)
    {
        if (string.IsNullOrEmpty(clerkUserId))
            throw new InvalidOperationException("Clerk user ID is missing.");

        var user = await _userRepository.GetByClerkIdAsync(clerkUserId);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        var scheduledClass = await _scheduledClassRepository.GetByIdAsync(dto.ScheduledClassId);
        if (scheduledClass == null)
            throw new InvalidOperationException("Scheduled class not found.");

        if (scheduledClass.ClassType.Capacity.HasValue &&
            scheduledClass.BookedSpots >= scheduledClass.ClassType.Capacity.Value)
            throw new InvalidOperationException("Class is fully booked.");

        var booking = await _bookingRepository.CreateAsync(user.Id, dto);

        if (booking != null)
        {
            scheduledClass.BookedSpots++;
            await _scheduledClassRepository.UpdateAsync(scheduledClass);
        }

        return booking;
    }

    public async Task<bool> DeleteBookingAsync(string? clerkUserId, int bookingId)
    {
        if (string.IsNullOrEmpty(clerkUserId))
            throw new InvalidOperationException("Clerk user ID is missing.");

        var user = await _userRepository.GetByClerkIdAsync(clerkUserId);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null || booking.UserId != user.Id)
            return false;

        var success = await _bookingRepository.DeleteAsync(bookingId, user.Id);

        if (success)
        {
            var scheduledClass = await _scheduledClassRepository.GetByIdAsync(booking.ScheduledClassId);
            if (scheduledClass != null && scheduledClass.BookedSpots > 0)
            {
                scheduledClass.BookedSpots--;
                await _scheduledClassRepository.UpdateAsync(scheduledClass);
            }
        }

        return success;
    }
}