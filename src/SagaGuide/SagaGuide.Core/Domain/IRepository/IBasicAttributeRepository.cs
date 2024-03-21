namespace SagaGuide.Core.Domain.IRepository;

public interface IBasicAttributeRepository : IRepository
{
    public Task<List<Attribute>> GetAllAttributesAsync(CancellationToken cancellationToken);
}