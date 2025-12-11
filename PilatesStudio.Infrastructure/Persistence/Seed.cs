using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Infrastructure.Persistence
{
    public static class Seed
    {
        public static void ApplyClassTypesSeed(PilatesDbContext context)
        {
            if (context.ClassTypes.Any())
                return;

            var now = DateTime.UtcNow;
            var classTypes = new List<ClassType>
            {
                new ClassType { Title = "Reformer", Description = "High-intensity workout using the reformer machine", Duration = 60, Capacity = 12, CreatedAt = now, UpdatedAt = now },
                new ClassType { Title = "Matwork", Description = "Classical pilates on the mat for core strength", Duration = 45, Capacity = 8, CreatedAt = now, UpdatedAt = now },
                new ClassType { Title = "Barre", Description = "Dance-inspired pilates class using a ballet barre", Duration = 50, Capacity = 10, CreatedAt = now, UpdatedAt = now },
                new ClassType { Title = "Gardua Slings", Description = "Pilates class using Gardua slings for support and resistance", Duration = 55, Capacity = 6, CreatedAt = now, UpdatedAt = now }
            };

            context.ClassTypes.AddRange(classTypes);
            context.SaveChanges();
        }
        
        public static void ApplyScheduledClassesSeed(PilatesDbContext context)
        {
            if (context.ScheduledClasses.Any())
                return;

            var now = DateTime.UtcNow;
            var scheduledClasses = new List<ScheduledClass>
            {
                new ScheduledClass { ClassTypeId = 1, StartTime = now.AddDays(1).AddHours(8), BookedSpots = 0, CreatedAt = now, UpdatedAt = now },
                new ScheduledClass { ClassTypeId = 2, StartTime = now.AddDays(1).AddHours(10), BookedSpots = 3, CreatedAt = now, UpdatedAt = now },
                new ScheduledClass { ClassTypeId = 3, StartTime = now.AddDays(1).AddHours(14), BookedSpots = 5, CreatedAt = now, UpdatedAt = now },
                new ScheduledClass { ClassTypeId = 4, StartTime = now.AddDays(2).AddHours(9), BookedSpots = 2, CreatedAt = now, UpdatedAt = now },
                new ScheduledClass { ClassTypeId = 1, StartTime = now.AddDays(2).AddHours(16), BookedSpots = 8, CreatedAt = now, UpdatedAt = now },
                new ScheduledClass { ClassTypeId = 2, StartTime = now.AddDays(3).AddHours(11), BookedSpots = 0, CreatedAt = now, UpdatedAt = now }
            };

            context.ScheduledClasses.AddRange(scheduledClasses);
            context.SaveChanges();
        }
    }
}