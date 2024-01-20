using System.ComponentModel.DataAnnotations;
using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class Speaker : _AuditEntityBase<Guid>
    {

        [MaxLength(250)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "MobileNumber")]
        public string? MobileNumber { get; set; }
    }
}
