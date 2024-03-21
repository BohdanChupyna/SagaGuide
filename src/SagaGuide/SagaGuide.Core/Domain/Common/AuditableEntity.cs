using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SagaGuide.Core.Definitions;

namespace SagaGuide.Core.Domain.Common;

public class AuditableEntity : GuidEntity, IAuditable
{
    public AuditableEntity()
    {
    }

    public AuditableEntity(AuditableDefinitionBase definition)
    {
        CreatedBy = definition.CreatedBy;
        CreatedOn = definition.CreatedOn;
        ModifiedBy = definition.ModifiedBy;
        //ModifiedOn = definition.ModifiedOn;
    }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public Guid ModifiedBy { get; set; } = Guid.Empty;

    [Timestamp]
    public uint Version { get; set; }

    protected void Update(AuditableDefinitionBase definition)
    {
        ModifiedBy = definition.ModifiedBy;
        ModifiedOn = definition.ModifiedOn;
    }
}