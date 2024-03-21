using System.ComponentModel.DataAnnotations;
using SagaGuide.Core.Definitions;

namespace SagaGuide.Core.Domain.Common;

public class AuditableAndTennableEntity : Entity<Guid>, IAuditable, ITenantable
{
    public AuditableAndTennableEntity()
    {
    }

    public AuditableAndTennableEntity(AuditableAndTennableDefinitionBase definition)
    {
        CreatedBy = definition.CreatedBy;
        CreatedOn = definition.CreatedOn;
        ModifiedBy = definition.ModifiedBy;
        ModifiedOn = definition.ModifiedOn;
        TenantId = definition.TenantId;
    }

    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Guid ModifiedBy { get; set; }
    public Guid TenantId { get; set; }
    
    [Timestamp]
    public uint Version { get; set; }

    protected void Update(AuditableAndTennableDefinitionBase definition)
    {
        ModifiedBy = definition.ModifiedBy;
        ModifiedOn = definition.ModifiedOn;
    }
}