using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Services;

public class BookingService(IUnitOfWork unitOfWork) : IBookingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<Booking>> GetUserBookingsAsync(string? clerkUserId)
    {
        if (string.IsNullOrEmpty(clerkUserId))
            throw new InvalidOperationException("Clerk user ID is missing.");

        var user = await _unitOfWork.Users.GetByClerkIdAsync(clerkUserId);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        return await _unitOfWork.Bookings.GetUserBookingsAsync(user.Id);
    }

    public async Task<Booking?> CreateBookingAsync(string? clerkUserId, CreateBookingDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(clerkUserId))
                throw new InvalidOperationException("Clerk user ID is missing.");

            var user = await _unitOfWork.Users.GetByClerkIdAsync(clerkUserId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            var scheduledClass = await _unitOfWork.ScheduledClasses.GetByIdAsync(dto.ScheduledClassId);
            if (scheduledClass == null)
                throw new InvalidOperationException("Scheduled class not found.");

            if (scheduledClass.ClassType.Capacity.HasValue &&
                scheduledClass.BookedSpots >= scheduledClass.ClassType.Capacity.Value)
                throw new InvalidOperationException("Class is fully booked.");

            var booking = await _unitOfWork.Bookings.CreateAsync(user.Id, dto);

            if (booking != null)
            {
                scheduledClass.BookedSpots++;
                await _unitOfWork.ScheduledClasses.UpdateAsync(scheduledClass);
            }

            await _unitOfWork.CommitTransactionAsync();
            return booking;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteBookingAsync(string? clerkUserId, int bookingId)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrEmpty(clerkUserId))
                throw new InvalidOperationException("Clerk user ID is missing.");

            var user = await _unitOfWork.Users.GetByClerkIdAsync(clerkUserId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null || booking.UserId != user.Id)
                return false;

            var success = await _unitOfWork.Bookings.DeleteAsync(bookingId, user.Id);

            if (success)
            {
                var scheduledClass = await _unitOfWork.ScheduledClasses.GetByIdAsync(booking.ScheduledClassId);
                if (scheduledClass != null && scheduledClass.BookedSpots > 0)
                {
                    scheduledClass.BookedSpots--;
                    await _unitOfWork.ScheduledClasses.UpdateAsync(scheduledClass);
                }
            }

            await _unitOfWork.CommitTransactionAsync();
            return success;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}