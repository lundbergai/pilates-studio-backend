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
                new ClassType { Title = "Gardua", Description = "Pilates class using Gardua slings for support and resistance", Duration = 55, Capacity = 6, CreatedAt = now, UpdatedAt = now }
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
        
        public static void ApplyUsersSeed(PilatesDbContext context)
        {
            if (context.Users.Any())
                return;
        
            var now = DateTime.UtcNow;
            var users = new List<User>
            {
                new User
                {
                    FullName = "Local Admin",
                    Email = "admin@pilatesstudio.com",
                    ClerkUserId = null,
                    IsAdmin = true,
                    IsInstructor = false,
                    IsMember = false,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new User
                {
                    FullName = "Sarah Johnson",
                    Email = "sarah.johnson@pilatesstudio.com",
                    ClerkUserId = null,
                    IsAdmin = false,
                    IsInstructor = true,
                    IsMember = false,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new User
                {
                    FullName = "John Smith",
                    Email = "john.smith@example.com",
                    ClerkUserId = null, 
                    IsAdmin = false,
                    IsInstructor = false,
                    IsMember = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new User
                {
                    FullName = "Emma Wilson",
                    Email = "emma.wilson@pilatesstudio.com",
                    ClerkUserId = null,
                    IsAdmin = false,
                    IsInstructor = true,
                    IsMember = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new User
                {
                    FullName = "Guest User",
                    Email = "guest@example.com",
                    ClerkUserId = null,
                    IsAdmin = false,
                    IsInstructor = false,
                    IsMember = false,
                    CreatedAt = now,
                    UpdatedAt = now
                }
            };
        
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}