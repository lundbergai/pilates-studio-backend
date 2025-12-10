using PilatesStudio.Domain.Entities;

namespace PilatesStudio.Infrastructure.Persistence
{
    public static class Seed
    {
        public static void ApplyClassTypesSeed(PilatesDbContext context)
        {
            if (context.ClassTypes.Any())
                return;

            var classTypes = new List<ClassType>
            {
                new ClassType { Title = "Reformer", Description = "High-intensity workout using the reformer machine", Duration = 60, Capacity = 12, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClassType { Title = "Matwork", Description = "Classical pilates on the mat for core strength", Duration = 45, Capacity = 8, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClassType { Title = "Barre", Description = "Dance-inspired pilates class using a ballet barre", Duration = 50, Capacity = 10, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClassType { Title = "Gardua Slings", Description = "Pilates class using Gardua slings for support and resistance", Duration = 55, Capacity = 6, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };

            context.ClassTypes.AddRange(classTypes);
            context.SaveChanges();
        }
    }
}