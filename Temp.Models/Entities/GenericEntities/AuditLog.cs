using System.ComponentModel.DataAnnotations.Schema;
using Temp.Models.Entities.Models;
using Temp.Models.Enums;

namespace Temp.Models.Entities.GenericEntities
{
    public class AuditLog : _EntityBase<Guid>
    {
        #region Propties
        public string TableName { get; set; }
        public Guid PrimaryKeyValue { get; set; }
        public AuditTypeEnum Type { get; set; }
        public DateTime ActionDate { get; set; }
        public Guid? UserId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        #endregion Proprties

        #region Navigation Propties
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion Navigation Proprties

    }
}
