using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tem.Base.Interfaces;

namespace Temp.Models.Base
{
    public class BaseEntity : IEntity
    {
    }
    public class BaseEntity<TKey> : BaseEntity, IEntity<TKey> where TKey : struct
    {
        [Key]
        [Column(Order = 0)]
        public TKey Id { get; set; }
    }
}
