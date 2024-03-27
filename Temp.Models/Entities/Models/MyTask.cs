using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class MyTask : _AuditEntityBase<Guid>
    {
        public string Description { get; set; }
        public int? CalendarId { get; set; }
        public DateTime? EndDate { get; set; }
        public string Text { get; set; }
        public string Company { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Owner { get; set; }
        public TaskStatus Status { get; set; }
        public Guid ParentId { get; set; }
        public int Progress { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }

    public enum TaskPriority
    {
        Low,
        Normal,
        High
    }

    public enum TaskStatus
    {
        Open,
        InProgress,
        Deferred,
        Completed
    }
}
