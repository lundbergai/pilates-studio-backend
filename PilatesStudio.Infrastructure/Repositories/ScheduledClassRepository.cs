using Microsoft.EntityFrameworkCore;
using PilatesStudio.Application.Dtos;
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
            .Include(sc => sc.Instructor)
            .ToListAsync();
    }
    
    public async Task<ScheduledClass?> GetByIdAsync(int id)
    {
        return await _context.ScheduledClasses
            .Include(sc => sc.ClassType)
            .Include(sc => sc.Instructor)
            .FirstOrDefaultAsync(sc => sc.Id == id);
    }
    
    public async Task<ScheduledClass> CreateAsync(CreateScheduledClassDto dto)
    {
        var scheduledClass = new ScheduledClass
        {
            ClassTypeId = dto.ClassTypeId,
            InstructorId = dto.InstructorId,
            StartTime = DateTime.SpecifyKind(DateTime.Parse(dto.StartTime), DateTimeKind.Utc),
            BookedSpots = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    
        await _context.ScheduledClasses.AddAsync(scheduledClass);
        await _context.SaveChangesAsync();
    
        return await _context.ScheduledClasses
            .Include(sc => sc.ClassType)
            .Include(sc => sc.Instructor)
            .FirstAsync(sc => sc.Id == scheduledClass.Id);
    }
    
    public async Task<ScheduledClass?> UpdateAsync(int id, UpdateScheduledClassDto dto)
    {
        var scheduledClass = await _context.ScheduledClasses.FindAsync(id);
        
        if (scheduledClass == null)
            return null;
    
        if (dto.ClassTypeId.HasValue)
            scheduledClass.ClassTypeId = dto.ClassTypeId.Value;
        
        if (dto.InstructorId.HasValue)
            scheduledClass.InstructorId = dto.InstructorId.Value;
        
        if (!string.IsNullOrEmpty(dto.StartTime))
            scheduledClass.StartTime = DateTime.SpecifyKind(DateTime.Parse(dto.StartTime), DateTimeKind.Utc);
    
        scheduledClass.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
    
        return await _context.ScheduledClasses
            .Include(sc => sc.ClassType)
            .Include(sc => sc.Instructor)
            .FirstAsync(sc => sc.Id == id);
    }
    
    public async Task<ScheduledClass?> UpdateAsync(ScheduledClass scheduledClass)
    {
        _context.ScheduledClasses.Update(scheduledClass);
        await _context.SaveChangesAsync();
        
        return await _context.ScheduledClasses
            .Include(sc => sc.ClassType)
            .Include(sc => sc.Instructor)
            .FirstOrDefaultAsync(sc => sc.Id == scheduledClass.Id);
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var scheduledClass = await _context.ScheduledClasses.FindAsync(id);
        
        if (scheduledClass == null)
            return false;
    
        _context.ScheduledClasses.Remove(scheduledClass);
        await _context.SaveChangesAsync();
    
        return true;
    }
}