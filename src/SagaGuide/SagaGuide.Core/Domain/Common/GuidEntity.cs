namespace SagaGuide.Core.Domain.Common;

public class GuidEntity : Entity<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
}