namespace SagaGuide.Core.Domain.Prerequisite;

public class SkillPrerequisite : PrerequisiteBase
{
    public StringCriteria NameCriteria { get; set; } = null!;
    public IntegerCriteria? LevelCriteria { get; set; }
    public StringCriteria? SpecializationCriteria { get; set; }
    
    public override IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.Skill;
}