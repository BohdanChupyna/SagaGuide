namespace SagaGuide.Core.Domain.Prerequisite;

public class AttributePrerequisite : PrerequisiteBase
{
    public IntegerCriteria QualifierCriteria { get; set; } = null!;
    public Attribute.AttributeType Required { get; set; }
    public Attribute.AttributeType? CombinedWith { get; set; }
    public IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.Attribute;
}