using System.ComponentModel.DataAnnotations;

namespace Temp.Models.Entities.GenericEntities
{
    public class _AuditEntityBase<IdT> : IEntityBase<IdT>, IAuditEntityBase<IdT>
    {
        [Key]
        public IdT Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? CreatedById { get; set; } //Guid? smae user id in app
        public Guid? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? DeletionTime { get; set; }
    }

    public interface IAuditEntityBase<IdT>
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreationTime { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
