namespace SagaGuide.Core.Domain.Prerequisite;

public class TraitPrerequisite : PrerequisiteBase
{
    public StringCriteria NameCriteria { get; set; } = null!;
    public IntegerCriteria? LevelCriteria { get; set; }
    public StringCriteria? NotesCriteria { get; set; }
    public override IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.Trait;
}