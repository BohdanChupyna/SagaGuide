using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Core.Domain.TraitAggregate;

public class TraitModifierGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<TraitModifier> Modifiers = new();
    public List<BookReference> BookReferences { get; set; } = new();
}