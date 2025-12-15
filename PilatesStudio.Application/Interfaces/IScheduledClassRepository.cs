using PilatesStudio.Application.Dtos;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IScheduledClassRepository
{
    Task<IEnumerable<ScheduledClass>> GetAllAsync();
    Task<ScheduledClass?> GetByIdAsync(int id);
    Task<ScheduledClass> CreateAsync(CreateScheduledClassDto dto);
    Task<ScheduledClass?> UpdateAsync(int id, UpdateScheduledClassDto dto);
    Task<ScheduledClass?> UpdateAsync(ScheduledClass scheduledClass);
    Task<bool> DeleteAsync(int id);
}