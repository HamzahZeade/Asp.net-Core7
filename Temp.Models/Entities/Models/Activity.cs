using System.ComponentModel.DataAnnotations.Schema;
using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class Activity : _AuditEntityBase<Guid>
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; } // You can use DateTime instead of string if you prefer
        public string Manager { get; set; }

        public Guid MyTaskId { get; set; }

        [ForeignKey("MyTaskId")]
        public MyTask MyTask { get; set; }

        //public Activity(string name, object date, string manager)
        //{
        //    this.name = name;
        //    this.date = date;
        //    this.manager = manager;
        //}
    }
}
