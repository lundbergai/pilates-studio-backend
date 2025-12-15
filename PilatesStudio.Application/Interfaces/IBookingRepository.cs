using PilatesStudio.Application.Dtos;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>> GetUserBookingsAsync(int userId);
    Task<Booking?> GetByIdAsync(int bookingId);
    Task<Booking?> CreateAsync(int userId, CreateBookingDto dto);
    Task<bool> DeleteAsync(int bookingId, int userId);
}