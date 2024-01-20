using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Temp.Models.Entities.GenericEntities;
using Temp.Models.Entities.Models;
using Temp.Models.Enums;

namespace Temp.DAL.Data
{
    public partial class AuditableContext : DbContext
    {
        #region Constructor
        public AuditableContext(DbContextOptions options) : base(options)
        {
        }
        #endregion Constructor

        #region DbSet
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        #endregion DbSet

        #region Methods
        public virtual async Task<int> SaveChangesAsync(Guid userId)
        {
            OnBeforeSaveChanges(userId);
            var result = await base.SaveChangesAsync();

            return result;
        }

        private void OnBeforeSaveChanges(Guid userId)
        {
            try
            {
                ChangeTracker.DetectChanges();
                List<AuditEntry> auditEntries = new List<AuditEntry>();
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                        continue;

                    AuditEntry auditEntry = new AuditEntry(entry);
                    auditEntry.TableName = entry.Entity.GetType().Name;
                    auditEntry.UserId = userId;
                    auditEntries.Add(auditEntry);

                    foreach (PropertyEntry property in entry.Properties)
                    {
                        string propertyName = property.Metadata.Name;
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValue = (Guid)property.CurrentValue;
                            continue;
                        }

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.Type = AuditTypeEnum.Create;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                break;

                            case EntityState.Deleted:
                                auditEntry.Type = AuditTypeEnum.Delete;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                break;

                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.Type = AuditTypeEnum.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }
                    }
                }
                foreach (var auditEntry in auditEntries)
                {
                    AuditLogs.Add(auditEntry.ToAudit());
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return;
            }

        }
        #endregion Methods
    }
}
