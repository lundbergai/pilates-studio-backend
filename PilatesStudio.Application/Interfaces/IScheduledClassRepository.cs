using PilatesStudio.Application.Dtos;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IScheduledClassRepository
{
    Task<IEnumerable<ScheduledClass>> GetAllAsync();
    Task<ScheduledClass> CreateAsync(CreateScheduledClassDto dto);
    Task<ScheduledClass?> UpdateAsync(int id, UpdateScheduledClassDto dto);
    Task<bool> DeleteAsync(int id);
}