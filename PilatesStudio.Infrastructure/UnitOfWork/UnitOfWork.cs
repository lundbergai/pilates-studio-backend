using PilatesStudio.Application.Interfaces;
using PilatesStudio.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace PilatesStudio.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly PilatesDbContext _context;
    private IDbContextTransaction? _transaction;

    public IBookingRepository Bookings { get; }
    public IUserRepository Users { get; }
    public IScheduledClassRepository ScheduledClasses { get; }

    public UnitOfWork(
        PilatesDbContext context,
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        IScheduledClassRepository scheduledClassRepository)
    {
        _context = context;
        Bookings = bookingRepository;
        Users = userRepository;
        ScheduledClasses = scheduledClassRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction != null)
                await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
        finally
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
