using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IScheduledClassRepository
{
    Task<IEnumerable<ScheduledClass>> GetAllAsync();
}