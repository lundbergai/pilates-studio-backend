using Microsoft.EntityFrameworkCore;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;
using PilatesStudio.Infrastructure.Persistence;

namespace PilatesStudio.Infrastructure.Repositories;

public class ScheduledClassRepository(PilatesDbContext context) : IScheduledClassRepository
{
    private readonly PilatesDbContext _context = context;

    public async Task<IEnumerable<ScheduledClass>> GetAllAsync()
    {
        return await _context.ScheduledClasses
            .Include(sc => sc.ClassType)
            .ToListAsync();
    }
}