using System.ComponentModel.DataAnnotations;
using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class AppRole : _AuditEntityBase<Guid>
    {
        [Required]
        public string Key { get; set; }
        public int Value { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
}
