namespace SagaGuide.Core.Domain.Prerequisite;

public class SpellPrerequisite : PrerequisiteBase
{
    public enum ComparisonTypeEnum
    {
        Name,
        Tag,
        College,
        CollegeCount,
        Any,
    }

    public ComparisonTypeEnum ComparisonType { get; set; }
    public StringCriteria QualifierCriteria { get; set; } 
    public IntegerCriteria? QuantityCriteria { get; set; }
    public override IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.Spell;
}