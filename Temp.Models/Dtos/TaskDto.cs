namespace Temp.Models.Dtos
{
    //public class TaskDto
    //{
    //    public string Company { get; set; }
    //    public string DueDate { get; set; }
    //    public int Id { get; set; }
    //    public string Manager { get; set; }
    //    public string Owner { get; set; }
    //    public Guid? ParentId { get; set; }
    //    public TaskPriority Priority { get; set; }
    //    public int? Progress { get; set; }
    //    public string StartDate { get; set; }
    //    public TaskStatus Status { get; set; }
    //    public string Text { get; set; }
    //}
    public class TaskDto
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? dueDate { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string owner { get; set; }
        public string company { get; set; }
        public string manager { get; set; }
        public int? progress { get; set; }
        public Guid? parentId { get; set; }
    }
    public class TaskInput
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? dueDate { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string owner { get; set; }
        public string company { get; set; }
        public int? progress { get; set; }
        public Guid? parentId { get; set; }
    }
}
