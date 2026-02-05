using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RentARide.Domain.Common;
using RentARide.Domain.Entities;

namespace RentARide.Infrastructure.Persistence.Interceptors
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = context.ChangeTracker.Entries().ToList();
            var auditLogs = new List<AuditLog>();

            foreach (var entry in entries)
            {
                
                if (entry.Entity is ISoftDeletable softDeletable && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    softDeletable.IsDeleted = true;
                    softDeletable.DeletedAt = DateTime.UtcNow;
                }

                
                if (entry.Entity is IAuditable || entry.Entity is AuditLog) 
                {
                    
                    if (entry.Entity is AuditLog) continue;
                    
                    var auditLog = new AuditLog
                    {
                        EntityName = entry.Entity.GetType().Name,
                        Timestamp = DateTime.UtcNow,
                        Action = entry.State.ToString()
                    };
                    
                    
                     if (entry.Entity is BaseEntity baseEntity)
                    {
                        auditLog.EntityId = baseEntity.Id.ToString();
                        
                        if (entry.Entity is IAuditable auditable && (entry.State == EntityState.Modified || entry.State == EntityState.Added))
                        {
                            auditable.LastModifiedAt = DateTime.UtcNow;
                        }
                    }

                    
                    
                    if (entry.State == EntityState.Modified)
                    {
                        auditLog.Changes = $"Modified properties: {string.Join(", ", entry.Properties.Where(p => p.IsModified).Select(p => p.Metadata.Name))}";
                    }
                    else
                    {
                        auditLog.Changes = $"Entity {entry.State}";
                    }

                    if (entry.State != EntityState.Unchanged && entry.State != EntityState.Detached)
                    {
                        auditLogs.Add(auditLog);
                    }
                }
            }

            
            
            
            if (auditLogs.Any())
            {
                await context.Set<AuditLog>().AddRangeAsync(auditLogs, cancellationToken);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
