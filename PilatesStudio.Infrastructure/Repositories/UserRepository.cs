using Microsoft.EntityFrameworkCore;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;
using PilatesStudio.Infrastructure.Persistence;

namespace PilatesStudio.Infrastructure.Repositories;

public class UserRepository(PilatesDbContext context) : IUserRepository
{
    private readonly PilatesDbContext _context = context;

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByClerkIdAsync(string clerkId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => string.Equals(u.ClerkUserId, clerkId));
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }
}