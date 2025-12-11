using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
}