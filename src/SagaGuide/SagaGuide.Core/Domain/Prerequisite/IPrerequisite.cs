namespace SagaGuide.Core.Domain.Prerequisite;

public interface IPrerequisite
{
    public enum PrerequisiteTypeEnum
    {
        Attribute,
        ContainedWeight,
        ContainedQuantity,
        Skill,
        Spell,
        Trait,
        Group,
    }
    PrerequisiteTypeEnum PrerequisiteType { get; }
}