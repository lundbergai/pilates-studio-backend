using Microsoft.EntityFrameworkCore;
using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Infrastructure.Persistence;

public class PilatesDbContext(DbContextOptions<PilatesDbContext> options) : DbContext(options)
{
    public DbSet<ClassType> ClassTypes { get; set; }
    public DbSet<ScheduledClass> ScheduledClasses { get; set; }
}