namespace SagaGuide.Core.Definitions;

public abstract class AuditableDefinitionBase
{
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public Guid ModifiedBy { get; set; } = Guid.Empty;
    public DateTime ModifiedOn { get; set; } = DateTime.Now;
}