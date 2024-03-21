namespace SagaGuide.Core.Definitions;

public abstract class AuditableAndTennableDefinitionBase : AuditableDefinitionBase
{
    public Guid TenantId { get; set; } = Guid.Empty;
}