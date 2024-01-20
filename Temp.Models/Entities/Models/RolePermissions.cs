using Temp.Models.Base;

namespace Temp.Models.Entities.Models
{
    public class RolePermission : BaseEntity<Guid>
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public AppRole Role { get; set; }

        public Permission Permission { get; set; }



    }

}
