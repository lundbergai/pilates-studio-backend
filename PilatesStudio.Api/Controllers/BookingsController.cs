using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;
using System.Security.Claims;

namespace PilatesStudio.Api.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize(Roles = "member")]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;
    
    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetMyBookings()
    {
        try
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var bookings = await _bookingService.GetUserBookingsAsync(clerkUserId);
            
            var bookingDtos = bookings.Select(BookingDto.FromBooking).ToList();
            return Ok(bookingDtos);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(CreateBookingDto dto)
    {
        try
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var booking = await _bookingService.CreateBookingAsync(clerkUserId, dto);
            
            var bookingDto = BookingDto.FromBooking(booking);
            return CreatedAtAction(nameof(Create), new { id = bookingDto.Id }, bookingDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> Delete(int bookingId)
    {
        try
        {
            var clerkUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var success = await _bookingService.DeleteBookingAsync(clerkUserId, bookingId);
            if (!success)
                return NotFound(new { message = "Booking not found." });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}