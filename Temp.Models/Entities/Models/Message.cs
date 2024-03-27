using System.ComponentModel.DataAnnotations.Schema;
using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class Message : _AuditEntityBase<Guid>
    {
        public string Manager { get; set; }
        public string Subject { get; set; }
        public DateTime? Date { get; set; } // You can use DateTime instead of string if you prefer
        public string Text { get; set; }
        public Guid MyTaskId { get; set; }

        [ForeignKey("MyTaskId")]
        public MyTask MyTask { get; set; }
    }
}
