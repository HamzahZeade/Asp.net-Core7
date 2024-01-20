using System.ComponentModel.DataAnnotations;

namespace Temp.Models.Entities.GenericEntities
{
    public class _EntityBase<IdT> : IEntityBase<IdT>
    {
        [Key]
        public IdT Id { get; set; }
        public bool IsDeleted { get; set; }
    }

    public interface IEntityBase<IdT> : IEntityBase
    {
        [Key]
        public IdT Id { get; set; }
    }

    public interface IEntityBase
    {
        public bool IsDeleted { get; set; }
    }
}
