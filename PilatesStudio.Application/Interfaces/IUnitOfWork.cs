namespace PilatesStudio.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookingRepository Bookings { get; }
    IUserRepository Users { get; }
    IScheduledClassRepository ScheduledClasses { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}