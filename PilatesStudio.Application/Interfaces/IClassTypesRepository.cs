using PilatesStudio.Application.Dtos;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IClassTypesRepository
{
    Task<IEnumerable<ClassType>> GetClassTypesAsync();
    Task<ClassType?> GetClassTypeAsync(int id);
    Task<ClassType> CreateClassTypeAsync(CreateClassTypeDto classTypeDto);
    Task<ClassType?> UpdateClassTypeAsync(int id, UpdateClassTypeDto classTypeDto);
    Task<bool> DeleteClassTypeAsync(int id);
}