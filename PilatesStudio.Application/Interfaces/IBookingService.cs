using PilatesStudio.Application.Dtos;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetUserBookingsAsync(string clerkUserId);
    Task<Booking?> CreateBookingAsync(string clerkUserId, CreateBookingDto dto);
    Task<bool> DeleteBookingAsync(string clerkUserId, int bookingId);
}