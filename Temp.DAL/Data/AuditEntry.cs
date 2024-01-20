using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Temp.Models.Entities.GenericEntities;
using Temp.Models.Enums;

namespace Temp.DAL.Data;

public class AuditEntry
{
    #region Proprties
    public EntityEntry Entry { get; }
    public Guid? UserId { get; set; }
    public string TableName { get; set; }
    public Guid KeyValue { get; set; }
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    public AuditTypeEnum Type { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    #endregion Proprties

    #region Constructor
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }
    #endregion Constructor

    #region Metods
    public AuditLog ToAudit()
    {
        AuditLog auditLog = new AuditLog();
        auditLog.UserId = UserId;
        auditLog.Type = Type;
        auditLog.TableName = TableName;
        auditLog.ActionDate = DateTime.Now;
        auditLog.PrimaryKeyValue = KeyValue;
        auditLog.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
        auditLog.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
        auditLog.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
        return auditLog;
    }
    #endregion Methods
}

