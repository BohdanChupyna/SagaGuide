using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Core.Domain.SkillAggregate;

public class SkillDefault
{
    public Attribute.AttributeType? AttributeType { get; set; }
    public string? Name { get; set; }
    public string? Specialization { get; set; }
    public int Modifier { get; set; }
    public int? Level { get; set; }
    public int? AdjustedLevel { get; set; }
    public int? Points { get; set; }
}