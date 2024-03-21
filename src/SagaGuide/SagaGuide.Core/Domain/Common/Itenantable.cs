namespace SagaGuide.Core.Domain.Common;

public interface ITenantable
{
    public Guid TenantId { get; set; }
}