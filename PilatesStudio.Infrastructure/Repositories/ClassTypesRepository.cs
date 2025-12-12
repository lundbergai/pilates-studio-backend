using Microsoft.EntityFrameworkCore;
using PilatesStudio.Application.Dtos;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Domain.Entities;
using PilatesStudio.Infrastructure.Persistence;

namespace PilatesStudio.Infrastructure.Repositories;

public class ClassTypesRepository(PilatesDbContext context) : IClassTypesRepository
{
    private readonly PilatesDbContext _context = context;

    public async Task<IEnumerable<ClassType>> GetClassTypesAsync()
    {
        return await _context.ClassTypes.ToListAsync();
    }

    public async Task<ClassType?> GetClassTypeAsync(int id)
    {
        return await _context.ClassTypes.FirstOrDefaultAsync(ct => ct.Id == id);
    }

    public async Task<ClassType> CreateClassTypeAsync(CreateClassTypeDto classTypeDto)
    {
        var classType = new ClassType
        {
            Title = classTypeDto.Title,
            Description = classTypeDto.Description,
            Duration = classTypeDto.Duration,
            Capacity = classTypeDto.Capacity,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.ClassTypes.Add(classType);
        await _context.SaveChangesAsync();

        return classType;
    }

    public async Task<ClassType?> UpdateClassTypeAsync(int id, UpdateClassTypeDto classTypeDto)
    {
        var existingClassType = await GetClassTypeAsync(id);
        if (existingClassType == null)
            return null;

        if (!string.IsNullOrWhiteSpace(classTypeDto.Title))
            existingClassType.Title = classTypeDto.Title;
    
        if (classTypeDto.Description != null)
            existingClassType.Description = classTypeDto.Description;
    
        if (classTypeDto.Duration.HasValue)
            existingClassType.Duration = classTypeDto.Duration;
    
        if (classTypeDto.Capacity.HasValue)
            existingClassType.Capacity = classTypeDto.Capacity;

        existingClassType.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingClassType;
    }


    public async Task<bool> DeleteClassTypeAsync(int id)
    {
        var classType = await GetClassTypeAsync(id);
        if (classType == null)
            return false;

        _context.ClassTypes.Remove(classType);
        await _context.SaveChangesAsync();

        return true;
    }
}
