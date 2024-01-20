using System.ComponentModel.DataAnnotations.Schema;
using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class UserRole : _AuditEntityBase<Guid>
    {
        #region Properties
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        #endregion Properties

        #region Navigation Properties

        [ForeignKey("RoleId")]
        public AppRole Role { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion Navigation Properties
    }
}
