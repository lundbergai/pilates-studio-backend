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
}