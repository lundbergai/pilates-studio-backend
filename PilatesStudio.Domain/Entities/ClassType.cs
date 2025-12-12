namespace PilatesStudio.Domain.Entities;

    public class ClassType
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public int? Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }