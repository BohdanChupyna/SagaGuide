namespace SagaGuide.Core.Domain.Common;

public class IdentifiableByGuid : IIdentifiable<Guid>
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    object IIdentifiable.Id => Id;
}